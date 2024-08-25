using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icechat.api.Models
{
    [Table("Groups")]
    public class Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GroupId { get; set; }

        [Required]
        public string? GroupName { get; set; }

        [Required]
        public long GroupOwner { get; set; }

        public string? GroupOwnerName { get; set; }
    }
}
