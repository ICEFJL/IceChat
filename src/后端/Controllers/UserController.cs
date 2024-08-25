
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Text;
using icechat.api.Models;
using icechat.api.Service;
using icechat.api.Utils;

namespace icechat.api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public Result<UserWithToken> Login([FromBody] LoginRequest request)
        {
            return _userService.Login(request.userName, request.password);
        }

        [HttpPost("registerByMail")]
        public Result<string> RegisterByMail([FromBody] RegisterByMailRequest request)
        {
            return _userService.RegisterByMail(request.UserName, request.NickName, request.Password, request.RePassword, request.Email, request.Code);
        }

        [HttpPost("register")]
        public Result<string> RegisterWithoutEmail([FromBody] RegisterWithoutEmailRequest request)
        {
            return _userService.RegisterWithoutEmail(request.UserName, request.Password, request.RePassword, request.NickName);
        }

        [HttpPost("ResetPassword")]
        public Result ResetPassword([FromBody] ResetPasswordRequest request)
        {
            return _userService.ResetPassword(request.UserName, request.OldPassword, request.Password, request.RePassword);
        }

        [HttpGet("searchUser")]
        public Result<List<Users>> SearchUser([FromQuery] string msg)
        {
            return _userService.SearchUser(msg);
        }

        [HttpGet("getFriendsList")]
        public Result<List<Users>> GetFriendsList([FromQuery] string userName)
        {
            return _userService.GetFriendsList(userName);
        }

        [HttpGet("userInfo")]
        public Result<Users> UserInfo([FromQuery] long userId)
        {
            return _userService.FindUserById(userId);
        }

        [HttpPost("update")]
        public Result Update([FromBody] Users user)
        {
            return _userService.Update(user);
        }

        [HttpPatch("updateAvatar")]
        public Result UpdateAvatar([FromQuery] string avatarUrl)
        {
            _userService.UpdateAvatar(avatarUrl);
            return Result.Success();
        }

        [HttpGet("codeByMail")]
        public Result SendCodeByMail([FromQuery] string mail)
        {
            return _userService.SendCodeByMail(mail);
        }

        [HttpGet("agreeAddUser")]
        public Result AgreeAddUser(long userId)
        {
            return _userService.AgreeAddUser(userId);
        }

        [HttpGet("disagreeAddUser")]
        public Result DisagreeAddUser(long userId)
        {
            return _userService.DisagreeAddUser(userId);
        }
        [HttpGet("getUser")]
        public Result<List<Users>> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpGet("getUserApplying")]
        public Result<List<Users>> GetUserApplying()
        {
            return _userService.GetUserApplying();
        }

        [HttpGet("blockUser")]

        public Result BlockUser(long userId)
        {
            return _userService.BlockUser(userId);
        }

        [HttpGet("unblockUser")]
        public Result UnBlockUser(long userId)
        {
            return _userService.UnBlockUser(userId);
        }

    }

    public class LoginRequest
    {
        [Required, RegularExpression("^\\S{5,32}$")]
        public string userName { get; set; }

        [Required, RegularExpression("^\\S{5,16}$")]
        public string password { get; set; }
    }

    public class RegisterByMailRequest
    {
        [Required, RegularExpression("^\\S{5,32}$")]
        public string UserName { get; set; }

        public string NickName { get; set; }

        [Required, RegularExpression("^\\S{5,16}$")]
        public string Password { get; set; }

        [Required]
        public string RePassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }
    }

    public class RegisterWithoutEmailRequest
    {
        [Required, RegularExpression("^\\S{5,32}$")]
        public string UserName { get; set; }

        [Required, RegularExpression("^\\S{5,16}$")]
        public string Password { get; set; }

        [Required]
        public string RePassword { get; set; }

        public string NickName { get; set; }
    }

    public class ResetPasswordRequest
    {
        [Required, RegularExpression("^\\S{5,32}$")]
        public string UserName { get; set; }

        [Required, RegularExpression("^\\S{5,16}$")]
        public string Password { get; set; }

        [Required]
        public string RePassword { get; set; }

        [Required, RegularExpression("^\\S{5,16}$")]
        public string OldPassword { get; set; }
    }
}
