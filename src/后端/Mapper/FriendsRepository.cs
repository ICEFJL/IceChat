using icechat.api.Models;
using icechat.api.Data;
using icechat.api.Utils;

namespace icechat.api.Mapper
{
    public interface IFriendsRepository
    {
        Friends SelectByUserId(long userId, long sendUserId);

        List<Users> SelectListByUserId(long userId);
        List<Users> SelectApplyingList(long userId);

        void Insert(Friends friends);

        void UpdateById(Friends friends);

        Friends SelectOne(Func<Friends, bool> predicate);

        int Delete(long userId1, long userId2);
    }

    public class FriendsRepository : IFriendsRepository
    {
        private readonly ApplicationDbContext _context;

        public FriendsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Friends SelectByUserId(long userId, long sendUserId)
        {
            return _context.Friends
                .FirstOrDefault(f => (f.UserId1 == userId && f.UserId2 == sendUserId) ||
                                     (f.UserId1 == sendUserId && f.UserId2 == userId));
        }

        public List<Users> SelectListByUserId(long userId)
        {
            List<long> users = _context.Friends
                .Where(f => f.Status == SystemConstants.STATUS_SUCCESS)
                .Where(f => f.UserId2 == userId || f.UserId1 == userId)
                .Select(f => f.UserId1 == userId ? f.UserId2 : f.UserId1)
                .ToList();
            return _context.Users
                .Where(u => users.Contains(u.UserId))
                .ToList();
        }

        public List<Users> SelectApplyingList(long userId)
        {
            List<long> users = _context.Friends
                .Where(f => f.UserId2 == userId && f.Status == SystemConstants.STATUS_APPLY)
                .Select(f => f.UserId1)
                .ToList();
            return _context.Users
                .Where(u => users.Contains(u.UserId))
                .ToList();
        }

        public void Insert(Friends friends)
        {
            _context.Friends.Add(friends);
            _context.SaveChanges();
        }

        public void UpdateById(Friends friends)
        {
            _context.Friends.Update(friends);
            _context.SaveChanges();
        }

        public Friends SelectOne(Func<Friends, bool> predicate)
        {
            return _context.Friends.FirstOrDefault(predicate);
        }

        public int Delete(long userId1, long userId2)
        {
            var friendsToDelete = SelectByUserId(userId1,userId2);
            _context.Friends.RemoveRange(friendsToDelete);
            return _context.SaveChanges();
        }
    }
}
