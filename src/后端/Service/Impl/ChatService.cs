
using icechat.api.Data;
using icechat.api.Mapper;
using icechat.api.Models;
using icechat.api.Utils;

namespace icechat.api.Service.Impl
{
    public class ChatService : IChatService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IPrivateMessagesRepository _privateMessagesRepository;
        private readonly IGroupMessagesRepository _groupMessagesRepository;
        private readonly IUserGroupsRepository _userGroupsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;


        public ChatService(ApplicationDbContext context, IFriendsRepository friendsRepository, IPrivateMessagesRepository privateMessagesRepository, IGroupMessagesRepository groupMessagesRepository, IUserGroupsRepository userGroupsRepository,IUserRepository userRepository)
        {
            _context = context;
            _friendsRepository = friendsRepository;
            _privateMessagesRepository = privateMessagesRepository;
            _groupMessagesRepository = groupMessagesRepository;
            _userGroupsRepository = userGroupsRepository;
            _userRepository = userRepository;
        }

        public Result<List<PrivateMessages>> GetPrivateMessage(long userId, long sendUserId)
        {
            var privateMessages = _privateMessagesRepository.SelectByUserId(userId, sendUserId);
            return Result.Success(privateMessages);
        }

        public Result<List<GroupMessages>> GetGroupsMessage(long userId, long groupId)
        {
            var userGroups = _userGroupsRepository.SelectByUserIdAndGroupId(userId, groupId);
            var groupMessages = _groupMessagesRepository.SelectByGroupId(groupId);
            var user = _userRepository.SelectById(userId);
            if (userGroups == null&&user.Role==SystemConstants.ROLE_USER)
            {
                return Result.Error("用户不在该群中",groupMessages);
            }
            return Result.Success(groupMessages);
        }

        public Result SendPrivateMessage(long userId, long toUserId, string msg)
        {
            Users sender = _userRepository.SelectById(userId);
            var privateMessage = new PrivateMessages
            {
                SenderId = userId,
                ReceiverId = toUserId,
                MessageText = msg,
                NickName = sender.NickName,
                MessageTime = DateTime.Now,
                sender = sender
            };
            _privateMessagesRepository.Insert(privateMessage);
            var friends = _friendsRepository.SelectByUserId(userId, toUserId);
            if (friends != null)
            {
                return Result.Success();
            }
            return Result.Error("发送异常");
        }

        public Result SendGroupsMessage(long userId, long toGroupId, string msg)
        {
            Users sender = _userRepository.SelectById(userId);
            Groups group = _context.Groups
                .FirstOrDefault(g => g.GroupId == toGroupId);
            if (sender == null||group == null)
            {
                return Result.Error("不存在对应用户或群组");
            }
            var groupMessage = new GroupMessages
            {
                SenderId = userId,
                GroupId = toGroupId,
                MessageText = msg,
                NickName = sender.NickName,
                MessageTime = DateTime.Now,
            };
            _groupMessagesRepository.Insert(groupMessage);
            return Result.Success();
        }

        public Result<List<PrivateMessages>> SearchPrivateMessage(long userId, long fromUserId,string msg)
        {
            var privateMessages = _privateMessagesRepository.SelectByMsg(userId, fromUserId, msg);
            return Result.Success(privateMessages);
        }

        public Result<List<GroupMessages>> SearchGroupsMessage(long groupId, string msg)
        {
            var groupMessages = _groupMessagesRepository.SelectByMsg(groupId,msg);
            return Result.Success(groupMessages);
        }

        public Result RemovePrivateMessage(long messageId)
        {
            var msg = _context.PrivateMessages.FirstOrDefault(ug => ug.MessageId == messageId);
            if (msg != null)
            {
                _context.PrivateMessages.Remove(msg);
                _context.SaveChanges();
                return Result.Success();
            }
            return Result.Error("删除失败");
        }

        public Result RemoveGroupsMessage(long messageId)
        {
            var msg = _context.GroupMessages.FirstOrDefault(ug => ug.MessageId == messageId);
            if (msg != null)
            {
                _context.GroupMessages.Remove(msg);
                _context.SaveChanges();
                return Result.Success();
            }
            return Result.Error("删除失败");
        }
    }
}