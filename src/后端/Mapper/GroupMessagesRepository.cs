using icechat.api.Data;
using icechat.api.Models;

namespace icechat.api.Mapper
{
    public interface IGroupMessagesRepository
    {
        List<GroupMessages> SelectByMsg(long groupId, string msg);
        List<GroupMessages> SelectByGroupId(long groupId);

        void Insert(GroupMessages groupMessages);
    }

    public class GroupMessagesRepository : IGroupMessagesRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupMessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GroupMessages> SelectByMsg(long groupId, string msg)
        {
            return _context.GroupMessages
                .Where(gm => gm.GroupId == groupId && gm.MessageText.Contains(msg))
                .OrderByDescending(gm => gm.MessageTime)
                .ToList();
        }

        public List<GroupMessages> SelectByGroupId(long groupId)
        {
            return _context.GroupMessages
                .Where(gm => gm.GroupId == groupId)
                .OrderByDescending(gm => gm.MessageTime)
                .ToList();
        }



        public void Insert(GroupMessages groupMessages)
        {
            _context.GroupMessages.Add(groupMessages);
            _context.SaveChanges();
        }
    }
}
