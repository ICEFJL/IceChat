using icechat.api.Models;

namespace icechat.api.Service
{
    public interface IFriendsService
    {
        Result AddFriend(long userId1, long userId2);

        Result AgreeFriend(long userId1, long userId2);

        Result DisagreeFriend(long userId1, long userId2);

        Result<List<Users>> GetFriendsList(long userId);

        Result RemoveFriend(long userId1, long userId2);

        Result<List<Users>> GetFriendApplying(long userId);

        public Result<List<Friends>> GetFriendsList();

        public Result<List<Friends>> SearchAllFriend(string msg);
    }
}
