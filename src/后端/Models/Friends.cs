using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icechat.api.Models
{
    [Table("Friends")]
    public class Friends
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FriendId { get; set; }

        [Required]
        public long UserId1 { get; set; }

        public string? UserName1 { get; set; }

        [Required]
        public long UserId2 { get; set; }

        public string? UserName2 {  get; set; }

        public string? Status { get; set; }

        public int MessageNum { get; set; }
    }
}
