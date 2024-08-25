using icechat.api.Models;

namespace icechat.api.Service
{
    public interface IGroupsService
    {
        Result<List<Groups>> GetGroupsList(long userId);

        Result<Groups> CreateGroups(long userId, string groupName);

        Result DeleteGroups(long userId, long groupId);

        Result AddGroups(long userId, long groupId);

        Result AgreeAddGroups(long userId, long groupId, long groupOwner);

        Result DisagreeAddGroups(long userId, long groupId, long groupOwner);

        Result<List<Users>> GetGroupsUserList(long groupId);

        Result<List<Groups>> SearchGroups(string msg);

        Result DeleteGroupsUser(long userId, long groupId, long groupOwner);

        Result<List<UserGroups>> GetGroupsApplyingList(long groupId);

        Result QuitGroups(long userId, long groupId);

        public Result<List<Groups>> GetAllGroups();
    }
}
