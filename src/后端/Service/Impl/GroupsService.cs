using icechat.api.Data;
using icechat.api.Models;
using icechat.api.Utils;

namespace icechat.api.Service.Impl
{
    public class GroupsService : IGroupsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GroupsService> _logger;

        public GroupsService(ApplicationDbContext context, ILogger<GroupsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Result<List<Groups>> GetGroupsList(long userId)
        {
            List<long> groupid =_context.UserGroups.Where(ug => ug.UserId == userId && ug.Status == SystemConstants.STATUS_SUCCESS).Select(ug => ug.GroupId).ToList();
            var groups = _context.Groups.Where(g => groupid.Contains(g.GroupId)).ToList();
            return Result.Success(groups);
        }

        public Result<Groups> CreateGroups(long userId, string groupName)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == groupName);
            if (existingGroup != null)
            {
                return Result.Error("该群已存在", existingGroup);
            }
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            var group = new Groups
            {
                GroupName = groupName,
                GroupOwner = userId,
                GroupOwnerName = user.UserName
            };
            _context.Groups.Add(group);
            _context.SaveChanges();
            var newGroup = _context.Groups.FirstOrDefault(g => g.GroupName == groupName);
            
            var userGroup = new UserGroups
            {
                UserId = userId,
                GroupId = newGroup.GroupId,
                Status = SystemConstants.STATUS_SUCCESS,
                MessageNum = 0,
                UserName = user.UserName,
                GroupName = groupName,
            };
            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();

            return Result.Success(newGroup);
        }

        public Result DeleteGroups(long userId, long groupId)
        {
            var group = _context.Groups.Find(groupId);
            if (group != null&&group.GroupOwner == userId)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
                return Result.Success();
            }
            else
            {
               return QuitGroups(userId, groupId);
            }
        }

        public Result AddGroups(long userId, long groupId)
        {
            var group = _context.Groups.Find(groupId);
            var userGroup = _context.UserGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup == null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
                var newUserGroup = new UserGroups
                {
                    UserId = userId,
                    GroupId = groupId,
                    Status = SystemConstants.STATUS_APPLY,
                    UserName = user.UserName,
                    GroupName = group.GroupName,
                };
                _context.UserGroups.Add(newUserGroup);
                _context.SaveChanges();
                return Result.Success();
            }
            if(userGroup.Status == SystemConstants.STATUS_APPLY)
            {
                return Result.Error("用户已申请");
            }
            return Result.Error("用户已在该群中");
        }

        public Result AgreeAddGroups(long userId, long groupId, long groupOwner)
        {
            var userGroup = _context.UserGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup == null || userGroup.Status != SystemConstants.STATUS_APPLY)
            {
                return Result.Error("用户请求异常");
            }

            var group = _context.Groups.Find(groupId);
            if (group.GroupOwner != groupOwner)
            {
                return Result.Error("你没有权限");
            }

            userGroup.Status = SystemConstants.STATUS_SUCCESS;
            _context.SaveChanges();
            return Result.Success();
        }

        public Result DisagreeAddGroups(long userId, long groupId, long groupOwner)
        {
            var userGroup = _context.UserGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup == null || userGroup.Status != SystemConstants.STATUS_APPLY)
            {
                return Result.Error("用户请求异常");
            }

            var group = _context.Groups.Find(groupId);
            if (group.GroupOwner != groupOwner)
            {
                return Result.Error("你没有权限");
            }

            _context.UserGroups.Remove(userGroup);
            _context.SaveChanges();
            return Result.Success();
        }

        public Result<List<Users>> GetGroupsUserList(long groupId)
        {
            List<long> userid=_context.UserGroups.Where(ug => ug.GroupId == groupId).Select(ug => ug.UserId).ToList();
            var users = _context.Users.Where(u => userid.Contains(u.UserId)).ToList();
            return Result.Success(users);
        }

        public Result<List<UserGroups>> GetGroupsApplyingList(long userId)
        {
            List<long> groupid = _context.Groups.Where(g => g.GroupOwner == userId).Select(g => g.GroupId).ToList();
            var userGroups = _context.UserGroups.Where(ug => groupid.Contains(ug.GroupId) && ug.Status == SystemConstants.STATUS_APPLY).ToList();
            return Result.Success(userGroups);
        }

        public Result QuitGroups(long userId, long groupId)
        {
            var userGroup = _context.UserGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup != null)
            {
                _context.UserGroups.Remove(userGroup);
                _context.SaveChanges();
                return Result.Success();
            }
            return Result.Error("退出失败");
        }

        public Result<List<Groups>> SearchGroups(string msg)
        {
            long UID = NumberUtils.ParseToLongOrDefault(msg);
            var groups = _context.Groups.Where(g => g.GroupName.Contains(msg)).ToList();
            var group = _context.Groups.Find(UID);
            if (group != null)
            {
                groups.Add(group);
            }
            return Result.Success(groups);
        }

        public Result DeleteGroupsUser(long userId, long groupId, long groupOwner)
        {
            var group = _context.Groups.Find(groupId);
            if (group.GroupOwner != groupOwner)
            {
                return Result.Error("你没有权限");
            }

            var userGroup = _context.UserGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup == null)
            {
                return Result.Error("该用户不存在");
            }

            _context.UserGroups.Remove(userGroup);
            _context.SaveChanges();
            return Result.Success();
        }

        public Result<List<Groups>> GetAllGroups()
        {
            return Result.Success(_context.Groups.ToList());
        }
    }
}
