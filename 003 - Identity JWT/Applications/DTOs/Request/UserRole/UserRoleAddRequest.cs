using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.DTOs.Request.UserRole
{
    public class UserRoleAddRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}