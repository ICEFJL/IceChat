using Microsoft.AspNetCore.Mvc;
using icechat.api.Models;
using icechat.api.Service;

namespace icechat.api.Controllers
{
    [ApiController]
    [Route("friend")]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet("addFriend")]
        public Result AddFriend(long userId1, long userId2)
        {
            return _friendsService.AddFriend(userId1, userId2);
        }

        [HttpGet("agreeFriend")]
        public Result AgreeFriend(long userId1, long userId2)
        {
            return _friendsService.AgreeFriend(userId1, userId2);
        }

        [HttpGet("disagreeFriend")]
        public Result DisagreeFriend(long userId1, long userId2)
        {
            return _friendsService.DisagreeFriend(userId1, userId2);
        }

        [HttpGet("getFriendsList")]
        public Result<List<Users>> GetFriendsList(long userId)
        {
            return _friendsService.GetFriendsList(userId);
        }

        [HttpGet("getAllFriends")]
        public Result<List<Friends>> GetFriendsList()
        {
            return _friendsService.GetFriendsList();
        }

        [HttpGet("removeFriend")]
        public Result RemoveFriend(long userId1, long userId2)
        {
            return _friendsService.RemoveFriend(userId1, userId2);
        }

        [HttpGet("getFriendApplying")]
        public Result<List<Users>> GetFriendApplying(long userId)
        {
            return _friendsService.GetFriendApplying(userId);
        }

        [HttpGet("searchAllFriend")]
        public Result<List<Friends>> SearchAllFriend(string msg)
        {
            return _friendsService.SearchAllFriend(msg);
        }
    }
}
