using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icechat.api.Models
{
    [Table("UserGroups")]
    public class UserGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserGroupsId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required] 
        public string? UserName { get; set; }

        [Required]
        public long GroupId { get; set; }

        public string? GroupName { get; set; }

        public string? Status { get; set; }

        public int MessageNum { get; set; }
    }
}
