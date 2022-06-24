using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class RegisterViewModel
    {
      //  public int Id { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [MinLength(6)]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
