using icechat.api.Models;

namespace icechat.api.Service
{
    public interface IChatService
    {
        Result<List<PrivateMessages>> GetPrivateMessage(long userId, long sendUserId);
        Result<List<GroupMessages>> GetGroupsMessage(long userId, long groupId);
        Result SendPrivateMessage(long userId, long toUserId, string msg);
        Result SendGroupsMessage(long userId, long toGroupId, string msg);
        Result<List<PrivateMessages>> SearchPrivateMessage(long userId, long toUserId, string msg);
        Result<List<GroupMessages>> SearchGroupsMessage(long groupId, string msg);
        public Result RemovePrivateMessage(long messageId);

        public Result RemoveGroupsMessage(long messageId);
    }
}
