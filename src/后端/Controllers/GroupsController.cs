using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using icechat.api.Models;
using icechat.api.Service;

namespace icechat.api.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpGet("getGroupsList")]
        public Result<List<Groups>> GetGroupsList(long userId)
        {
            return _groupsService.GetGroupsList(userId);
        }

        [HttpGet("createGroups")]
        public Result<Groups> CreateGroups(long groupOwner, string groupName)
        {
            return _groupsService.CreateGroups(groupOwner, groupName);
        }

        [HttpGet("deleteGroups")]
        public Result DeleteGroups(long userId, long groupId)
        {
            return _groupsService.DeleteGroups(userId, groupId);
        }

        [HttpGet("addGroups")]
        public Result AddGroups(long userId, long groupId)
        {
            return _groupsService.AddGroups(userId, groupId);
        }

        [HttpGet("agreeAddGroups")]
        public Result AgreeAddGroups(long userId, long groupId, long groupOwner)
        {
            return _groupsService.AgreeAddGroups(userId, groupId, groupOwner);
        }

        [HttpGet("disagreeAddGroups")]
        public Result DisagreeAddGroups(long userId, long groupId, long groupOwner)
        {
            return _groupsService.DisagreeAddGroups(userId, groupId, groupOwner);
        }

        [HttpGet("deleteGroupsUser")]
        public Result DeleteGroupsUser(long userId, long groupId, long groupOwner)
        {
            return _groupsService.DeleteGroupsUser(userId, groupId, groupOwner);
        }

        [HttpGet("getGroupsUserList")]
        public Result<List<Users>> GetGroupsUserList(long groupId)
        {
            return _groupsService.GetGroupsUserList(groupId);
        }

        [HttpGet("getGroupsApplyingList")]
        public Result<List<UserGroups>> GetGroupsApplyingList(long userId)
        {
            return _groupsService.GetGroupsApplyingList(userId);
        }

        [HttpGet("searchGroupsByNameOrId")]
        public Result<List<Groups>> searchGroupsByNameOrId(string msg)
        {
            return _groupsService.SearchGroups(msg);
        }

        [HttpGet("quitGroups")]
        public Result QuitGroups(long userId, long groupId)
        {
            return _groupsService.QuitGroups(userId, groupId);
        }

        [HttpGet("getAllGroups")]
        public Result<List<Groups>> GetAllGroups()
        {
            return _groupsService.GetAllGroups();
        }
    }
}
