using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models.Requests
{
    public class CreateEmployeeDTO
    {
        public string FirstName { get; set; }
        
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Boss { get; set; }
        public int UserID { get; set; }
    }
}
