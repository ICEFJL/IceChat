using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using icechat.api.Models;
using icechat.api.Service;
using icechat.api.Utils;

namespace icechat.api.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("getPrivateMessage")]
        public Result<List<PrivateMessages>> GetPrivateMessage(long userId1, long userId2)
        {
            return _chatService.GetPrivateMessage(userId1, userId2);
        }

        [HttpGet("getGroupsMessage")]
        public Result<List<GroupMessages>> GetGroupsMessage(long userId, long groupId)
        {
            return _chatService.GetGroupsMessage(userId, groupId);
        }

        [HttpGet("sendPrivateMessage")]
        public Result SendPrivateMessage(long userId, long toUserId, string msg)
        {
            return _chatService.SendPrivateMessage(userId, toUserId, msg);
        }

        [HttpGet("sendGroupsMessage")]
        public Result SendGroupsMessage(long userId, long toGroupId, string msg)
        {
            return _chatService.SendGroupsMessage(userId, toGroupId, msg);
        }

        [HttpGet("searchPrivateMessage")]
        public Result<List<PrivateMessages>> SearchPrivateMessage(long userId, long toUserId, string msg)
        {
            return _chatService.SearchPrivateMessage(userId, toUserId, msg);
        }

        [HttpGet("searchGroupsMessage")]
        public Result<List<GroupMessages>> SearchGroupsMessage(long groupId, string msg)
        {
            return _chatService.SearchGroupsMessage(groupId, msg);
        }

        [HttpGet("removePrivateMessage")]
        public Result RemovePrivateMessage(long messageId)
        {
            return _chatService.RemovePrivateMessage(messageId);
        }

        [HttpGet("removeGroupsMessage")]
        public Result RemoveGroupsMessage(long messageId)
        {
            return _chatService.RemoveGroupsMessage(messageId);
        }
    }
}