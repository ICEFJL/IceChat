using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icechat.api.Models
{
    [Table("PrivateMessages")]
    public class PrivateMessages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MessageId { get; set; }

        [Required]
        public long SenderId { get; set; }

        [Required]
        public string? NickName { get; set; }

        [Required]
        public long ReceiverId { get; set; }

        [Required]
        public string? MessageText { get; set; }

        public DateTime MessageTime { get; set; }

        public string? MessageType { get; set; }

        public string? MessageStatus { get; set; }

        public Users sender { get; set; }
    }
}
