using icechat.api.Data;
using icechat.api.Models;
using icechat.api.Utils;

namespace icechat.api.Mapper
{
    public interface IUserGroupsRepository
    {
        UserGroups SelectByUserIdAndGroupId(long userId, long groupId);
        List<UserGroups> SelectApplyingByOwnerId(long userId);
    }

    public class UserGroupsRepository : IUserGroupsRepository
    {
        private readonly ApplicationDbContext _context;

        public UserGroupsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserGroups SelectByUserIdAndGroupId(long userId, long groupId)
        {
            return _context.UserGroups
                .FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
        }

        public List<UserGroups> SelectApplyingByOwnerId(long userId)
        {
            List<long> groups = _context.Groups
                .Where(g => g.GroupOwner == userId)
                .Select(g => g.GroupId)
                .ToList();
            return _context.UserGroups
                .Where(ug => groups.Contains(ug.GroupId) && ug.Status == SystemConstants.STATUS_APPLY)
                .ToList();
        }
    }
}
