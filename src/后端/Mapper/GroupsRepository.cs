using System.Text.RegularExpressions;
using icechat.api.Data;
using icechat.api.Models;

namespace icechat.api.Mapper
{
    public interface IGroupsRepository
    {
        List<Groups> SelectListByUserId(long userId);

    }

    public class GroupsRepository : IGroupsRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Groups> SelectListByUserId(long userId)
        {
            List<long> groups = _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.UserGroupsId)
                .ToList();
            return _context.Groups
                .Where(g => groups.Contains(g.GroupId))
                .ToList();
        }
    }
}
