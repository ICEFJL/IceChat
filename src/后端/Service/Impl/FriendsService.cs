
using Microsoft.EntityFrameworkCore;
using System;
using icechat.api.Mapper;
using icechat.api.Models;
using icechat.api.Data;
using icechat.api.Utils;

namespace icechat.api.Service.Impl
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public FriendsService(IFriendsRepository friendsRepository, IUserRepository userRepository, ApplicationDbContext context)
        {
            _friendsRepository = friendsRepository;
            _userRepository = userRepository;
            _context = context;
        }

        public Result AddFriend(long userId1, long userId2)
        {
            var friends1 = _friendsRepository.SelectOne(f => f.UserId1 == userId1 && f.UserId2 == userId2);
            if (friends1 != null && friends1.Status == SystemConstants.STATUS_SUCCESS)
            {
                return Result.Error("已添加该用户");
            }

            var user1 = _userRepository.SelectById(userId1);
            var user2 = _userRepository.SelectById(userId2);
            if (user1 == null || user2 == null)
            {
                return Result.Error("用户不存在");
            }

            var friends = new Friends
            {
                UserId1 = user1.UserId,
                UserId2 = user2.UserId,
                UserName1 = user1.UserName,
                UserName2 = user2.UserName,
                Status = SystemConstants.STATUS_APPLY
            };
            _friendsRepository.Insert(friends);
            return Result.Success();
        }

        public Result AgreeFriend(long userId1, long userId2)
        {
            var existingFriends = _friendsRepository.SelectByUserId(userId1, userId2);
            if (existingFriends != null)
            {
                existingFriends.Status = SystemConstants.STATUS_SUCCESS;
                _context.SaveChanges();
                return Result.Success();
            }
            else
            {
                return Result.Error("意外错误");
            }
            
        }

        public Result DisagreeFriend(long userId1, long userId2)
        {
            var result = _friendsRepository.Delete(userId1, userId2);
            if (result > 0)
            {
                return Result.Success();
            }
            else
            {
                return Result.Error("意外错误");
            }
        }

        public Result<List<Users>> GetFriendsList(long userId)
        {
            var users = _friendsRepository.SelectListByUserId(userId);
            return Result.Success(users);
        }

        public Result RemoveFriend(long userId1, long userId2)
        {
            var result = _friendsRepository.Delete(userId1, userId2);
            if (result > 0)
            {
                return Result.Success();
            }
            else
            {
                return Result.Error("意外错误");
            }
        }

        public Result<List<Users>> GetFriendApplying(long userId)
        {
            var friends = _friendsRepository.SelectApplyingList(userId);
            return Result.Success(friends);
        }

        public Result<List<Friends>> GetFriendsList()
        {
            return Result.Success(_context.Friends.ToList());
        }

        public Result<List<Friends>> SearchAllFriend(string msg)
        {
            long userId = NumberUtils.ParseToLongOrDefault(msg);
            var friends = _context.Friends
                .Where(f => f.Status == SystemConstants.STATUS_SUCCESS)
                .Where(f => f.UserId2 == userId || f.UserId1 == userId || f.UserName1.Contains(msg)||f.UserName2.Contains(msg))
                .ToList();
            return Result.Success(friends);
        }
    }
}