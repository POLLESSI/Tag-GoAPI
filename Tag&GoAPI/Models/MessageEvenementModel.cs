using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tag_GoAPI.Models
{
    public class MessageEvenementModel
    {
    #nullable disable
        [Required(ErrorMessage = "The message is required")]
        [MinLength(2)]
        [MaxLength(512)]
        public string NewMessage { get; set; }
        [Required(ErrorMessage = "The name of the author is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Author : ")]
        public string Author { get; set; }
        [DisplayName("Sending Date : ")]
        public DateTime SendingDate { get; set; }
        //[Required(ErrorMessage = "Event Id is required : ")]
        [DisplayName("Event Id : ")]
        public int Evenement_Id { get; set; }
    }
}
