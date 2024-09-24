using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs
{
    public class MessageActivity
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Message : ")]
        public string NewMessage { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Author : ")]
        public string Author { get; set; }
        public DateTime SendingDate { get; set; } = DateTime.UtcNow;
        [Required]
        [DisplayName("Activity Id : ")]
        public int Activity_Id { get; set; }
        [DisplayName("Is Private? : ")]
        public string IsPrivate { get; set; } = "No";
    }
}
