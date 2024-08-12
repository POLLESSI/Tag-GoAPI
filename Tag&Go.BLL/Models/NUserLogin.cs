using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag_Go.BLL.Models
{
    public class NUserLogin
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [MinLength(8)]
        [MaxLength(64)]
        [DisplayName("Email : ")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(64)]
        [DataType(DataType.Password)]
        [DisplayName("Password : ")]
        public string? Pwd { get; set; }
    }
}
