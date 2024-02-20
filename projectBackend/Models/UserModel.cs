using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models
{
    public class UserModel
    {
        [Key]public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Boss { get; set; }

    }
}
