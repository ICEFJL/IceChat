using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace icechat.api.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        public string? NickName { get; set; }

        public string? Phone { get; set; }

        public string? PasswordHash { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Role { get; set; }

        public string? Status {  get; set; }
    }
}
