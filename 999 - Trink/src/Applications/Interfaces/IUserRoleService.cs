using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.UserRole;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.UserRoleRepository;

namespace Applications.Interfaces
{
    public interface IUserRoleService
    {
        Task<IResult> AddUserRoleAsync(UserRoleAddRequest userRoleAddRequest);
    }
}