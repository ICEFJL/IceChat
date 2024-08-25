using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icechat.api.Models
{
    [Table("GroupMessages")]
    public class GroupMessages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MessageId { get; set; }

        [Required]
        public long SenderId { get; set; }

        [Required]
        public string? NickName { get; set; }

        [Required]
        public long GroupId { get; set; }

        [Required]
        public string? MessageText { get; set; }

        public DateTime MessageTime { get; set; }

        public string? MessageType { get; set; }
    }
}
