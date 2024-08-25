using icechat.api.Data;
using icechat.api.Mapper;
using icechat.api.Models;
using icechat.api.Utils;


namespace icechat.api.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ISession _session; //session对象
        private readonly IHttpContextAccessor _contextAccessor; //上下文对象


        public UserService(ApplicationDbContext context, IUserRepository userMapper, ILogger<UserService> logger, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userRepository = userMapper;
            _logger = logger;
            _contextAccessor = contextAccessor;
            _session = contextAccessor.HttpContext.Session;
        }

        public Users FindByUserName(string userName)
        {
            return _userRepository.FindByUserName(userName);
        }

        public Result<UserWithToken> Login(string username, string password)
        {
            UserWithToken userWithToken = new UserWithToken();
            var t_user = _userRepository.FindByUserName(username);
            if (t_user == null)
            {
                t_user = _userRepository.FindUserByEmail(username);
                if (t_user == null)
                {
                    return Result.Error("用户不存在",userWithToken);
                }
            }

            if (!t_user.PasswordHash.Equals(SHAUtil.SHA256Encrypt(password)))
            {
                return Result.Error("密码错误",userWithToken);
            }
            var token = CreateAndSetToken(t_user);
            _session.SetString("user", t_user.UserName);
            userWithToken = new UserWithToken
            {
                UserName = t_user.UserName,
                UserId = t_user.UserId,
                Token = token,
                NickName = t_user.NickName,
                Role = t_user.Role
            };
            if (t_user.Role == SystemConstants.ROLE_USER && (t_user.Status == SystemConstants.STATUS_APPLY ||t_user.Status == SystemConstants.STATUS_BLOCK)){
                return Result.Error("当前用户禁止登录",userWithToken);
            }
            
            return Result.Success(userWithToken);
        }

        private string CreateAndSetToken(Users user)
        {
            var token = Guid.NewGuid().ToString("N");
            return token;
        }

        public Result<string> RegisterWithoutEmail(string username, string password, string rePassword, string nickName)
        {
            string tokenKey = "";
            if (!rePassword.Equals(password))
            {
                return Result.Error("两次密码不一致", tokenKey);
            }

            var t_user = _userRepository.FindByUserName(username);
            if (t_user != null)
            {
                return Result.Error("用户已存在", tokenKey);
            }

            var user = new Users
            {
                UserName = username,
                NickName = nickName,
                PasswordHash = SHAUtil.SHA256Encrypt(password)
            };
            _userRepository.Insert(user);
            tokenKey = CreateAndSetToken(user);
            return Result.Success(tokenKey);
        }

        public Result Update(Users user)
        {
            var byUserName = FindByUserName(user.UserName);
            if (byUserName != null&&!byUserName.UserId.Equals(user.UserId))
            {
                return Result.Error("用户名已存在");
            }
           
            var t_user = _userRepository.SelectById(user.UserId);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }
            else
            {
                t_user.UserName = user.UserName;
                t_user.NickName = user.NickName;
                t_user.UpdatedTime = DateTime.Now;
                t_user.Phone = user.Phone;
                _userRepository.UpdateById(t_user);
                return Result.Success();
            }
        }

        public Result<List<Users>> SearchUser(string msg)
        {
            long UID = NumberUtils.ParseToLongOrDefault(msg);

            var users = _context.Users
                .Where(u =>( u.UserName.Contains(msg) || u.Email == msg || u.NickName.Contains(msg) || u.UserId == UID)&&u.Role==SystemConstants.ROLE_USER)
                .ToList();

            return Result.Success(users);
        }

        public void UpdateAvatar(string avatarUrl)
        {
            var uid ="";
            var id = long.Parse(uid.ToString());
            _userRepository.UpdateAvatar(avatarUrl, id);
        }

        public Result<List<Users>> GetFriendsList(string userName)
        {
            var user = _userRepository.FindByUserName(userName);
            var friends = _userRepository.SelectFriendList(user.UserId);
            return Result.Success(friends);
        }

        public Result SendCodeByMail(string toMail)
        {
            if (!RegexUtils.IsEmailInvalid(toMail))
            {
                return Result.Error("请输入正确的邮箱");
            }

            var code = RandomUtil.RandomNumbers(6);
            Console.WriteLine(code);
            CodeUtil.Set(toMail, code);
            _logger.LogDebug("发送短信验证码成功，验证码：{code}", code);
            SendMailUtils.SendText(code, toMail);
            return Result.Success();
        }

        public Result<string> RegisterByMail(string userName, string nickName, string password, string rePassword, string email, string code)
        {
            string token = "";
            var cacheCode = CodeUtil.Get(email);
            if (cacheCode == null || !cacheCode.Equals(code))
            {
                return Result.Error("验证码错误",token);
            }

            if (!rePassword.Equals(password))
            {
                return Result.Error("两次密码不一致",token);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                var t_user = _userRepository.FindByUserName(userName);
                if (t_user != null)
                {
                    return Result.Error("用户已存在",token);
                }
            }

            try
            {
                var user = new Users
                {
                    Email = email,
                    PasswordHash = SHAUtil.SHA256Encrypt(password),
                    UserName = userName,
                    NickName = string.IsNullOrEmpty(nickName) ? SystemConstants.USER_NICK_NAME_PREFIX + RandomUtil.RandomString(10) : nickName,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    Role = SystemConstants.ROLE_USER,
                    Status = SystemConstants.STATUS_APPLY
                };
                _userRepository.Insert(user);
                token = CreateAndSetToken(user);
                CodeUtil.Remove(email);
                return Result.Success(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public Result<Users> FindUserById(long userId)
        {
            var users = _userRepository.SelectById(userId);
            return Result.Success(users);
        }

        public Result ResetPassword(string userName, string oldPassword, string password, string rePassword)
        {
            var t_user = _userRepository.FindByUserName(userName);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }

            if (!t_user.PasswordHash.Equals(SHAUtil.SHA256Encrypt(oldPassword)))
            {
                return Result.Error("原密码错误");
            }

            if (!rePassword.Equals(password))
            {
                return Result.Error("两次密码不一致");
            }

            t_user.PasswordHash = SHAUtil.SHA256Encrypt(password);
            _userRepository.UpdateById(t_user);
            return Result.Success();
        }

        public Result AgreeAddUser(long userId)
        {
            var t_user = _userRepository.SelectById(userId);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }
            else
            {
                t_user.Status = SystemConstants.STATUS_SUCCESS;
                _userRepository.UpdateById(t_user);
                return Result.Success();
            }
        }

        public Result DisagreeAddUser(long userId)
        {
            var t_user = _userRepository.SelectById(userId);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }
            else
            {
                _context.Users.Remove(t_user);
                _context.SaveChanges();
                return Result.Success();
            }
        }

        public Result<List<Users>> GetUser()
        {
            
            var user= _context.Users
               .Where(u => (u.Status == SystemConstants.STATUS_SUCCESS || u.Status==SystemConstants.STATUS_BLOCK) && u.Role == SystemConstants.ROLE_USER)
               .ToList();
            return Result.Success(user);
        }

        public Result<List<Users>> GetUserApplying()
        {
            var user = _context.Users
             .Where(u => (u.Status == SystemConstants.STATUS_APPLY) && u.Role == SystemConstants.ROLE_USER)
             .ToList();
            return Result.Success(user);
        }

        public Result BlockUser(long userId)
        {
            var t_user = _userRepository.SelectById(userId);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }
            else
            {
                t_user.Status = SystemConstants.STATUS_BLOCK;
                _userRepository.UpdateById(t_user);
                return Result.Success();
            }
        }

        public Result UnBlockUser(long userId)
        {
            var t_user = _userRepository.SelectById(userId);
            if (t_user == null)
            {
                return Result.Error("用户不存在");
            }
            else
            {
                t_user.Status = SystemConstants.STATUS_SUCCESS;
                _userRepository.UpdateById(t_user);
                return Result.Success();
            }
        }
    }
}
