using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.DTOs.Response.UserRole
{
    public class UserRoleName
    {
        public Guid userId { get; set; }
        public Guid roleId { get; set; }
        public string RoleName { get; set; }
    }
}