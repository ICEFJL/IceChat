using icechat.api.Models;

namespace icechat.api.Service
{
    public interface IUserService
    {
        // 根据用户名查询用户
        Users FindByUserName(string userName);

        // 注册

        Result<string> RegisterWithoutEmail(string username, string password, string rePassword, string nickName);

        Result Update(Users user);

        Result<List<Users>> SearchUser(string msg);

        void UpdateAvatar(string url);

        Result<UserWithToken> Login(string username, string password);

        Result<List<Users>> GetFriendsList(string userName);

        Result SendCodeByMail(string mail);

        Result<string> RegisterByMail(string userName, string nickName, string password, string rePassword, string email, string code);

        Result<Users> FindUserById(long userId);

        Result ResetPassword(string userName, string oldPassword, string password, string rePassword);

        public Result AgreeAddUser(long userId);

        public Result DisagreeAddUser(long userId);

        public Result<List<Users>> GetUser();

        public Result<List<Users>> GetUserApplying();

        public Result BlockUser(long userId);

        public Result UnBlockUser(long userId);
    }
}
