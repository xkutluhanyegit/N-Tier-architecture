using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.Role;
using Domain.Entities;

namespace Applications.Interfaces
{
    public interface IRoleService
    {
        Task<IDataResult<IEnumerable<Role>>> GetAllRolesAsync();
        Task<IResult> AddRoleAsync(RoleAddRequest roleAddRequest);

        Task<Guid> GetByRoleIdAsync(string roleName);
        
    }
}