using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string MobilePhone { get; set; }
        [Required]
        [MinLength(5)]
        public string city { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public  ApplicationUsersIdentity user { get; set; }

    }
}
