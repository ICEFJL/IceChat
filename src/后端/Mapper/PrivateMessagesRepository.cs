using System;
using icechat.api.Data;
using icechat.api.Models;

namespace icechat.api.Mapper
{
    public interface IPrivateMessagesRepository
    {
        List<PrivateMessages> SelectByUserId(long userId, long sendUserId);
        List<PrivateMessages> SelectByMsg(long userId, long fromUserId,string msg);
        void Insert(PrivateMessages privateMessages);

        void UpdateById(PrivateMessages privateMessages);
    }

    public class PrivateMessagesRepository : IPrivateMessagesRepository
    {
        private readonly ApplicationDbContext _context;

        public PrivateMessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PrivateMessages> SelectByUserId(long userId1, long userId2)
        {
            return _context.PrivateMessages
                .Where(pm => (pm.SenderId == userId1 && pm.ReceiverId == userId2) ||
                             (pm.SenderId == userId2 && pm.ReceiverId == userId1))
                .OrderByDescending(pm => pm.MessageTime)
                .ToList();
        }

        public List<PrivateMessages> SelectByMsg(long userId1, long userId2, string msg)
        {
            return _context.PrivateMessages
                .Where(pm => ((pm.SenderId == userId1 && pm.ReceiverId == userId2) ||
                             (pm.SenderId == userId2 && pm.ReceiverId == userId1)) && pm.MessageText.Contains(msg))
                .OrderByDescending(pm => pm.MessageTime)
                .ToList();
        }

        public void Insert(PrivateMessages privateMessages)
        {
            _context.PrivateMessages.Add(privateMessages);
            _context.SaveChanges();
        }

        public void UpdateById(PrivateMessages privateMessages)
        {
            _context.PrivateMessages.Update(privateMessages);
            _context.SaveChanges();
        }
    }
}
