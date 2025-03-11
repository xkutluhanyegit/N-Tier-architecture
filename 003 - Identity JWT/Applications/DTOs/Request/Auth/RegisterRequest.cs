using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.DTOs.Request.Auth
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}