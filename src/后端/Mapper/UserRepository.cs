using icechat.api.Data;
using icechat.api.Models;
using icechat.api.Utils;

namespace icechat.api.Mapper
{
    public interface IUserRepository
    {
        Users FindByUserName(string userName);
        void UpdateAvatar(string avatarUrl, long id);
        void UpdatePwd(string passwordHash, long id);
        List<Users> SelectFriendList(long userId);
        List<Users> SelectListByGroups(long groupId);
        Users FindUserByEmail(string username);
        List<Users> GetUsersByIds(List<long> userIds);

        Users SelectById(long id);

        void Insert(Users user);

        void UpdateById(Users user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void UpdateById(Users user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public Users FindByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public void UpdateAvatar(string avatarUrl, long id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                user.AvatarUrl = avatarUrl;
                user.UpdatedTime = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void UpdatePwd(string passwordHash, long id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.UpdatedTime = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public List<Users> SelectFriendList(long userId)
        {
            List<long> users = _context.Friends
                .Where(f => f.UserId1 == userId || f.UserId2 == userId)
                .Select(f => f.UserId1 == userId ? f.UserId2 : f.UserId1)
                .ToList();
            return GetUsersByIds(users);
        }

        public List<Users> SelectListByGroups(long groupId)
        {
            List<long> users = _context.UserGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug.UserId)
                .ToList();
            return GetUsersByIds(users);
        }

        public Users FindUserByEmail(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Email == username);
        }

        public List<Users> GetUsersByIds(List<long> userIds)
        {
            return _context.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToList();
        }

        public Users SelectById(long id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }


        public void Insert(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
