namespace icechat.api.Models
{
    public class UserWithToken
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Token { get; set; }

        public string Role { get; set; }
    }
}
