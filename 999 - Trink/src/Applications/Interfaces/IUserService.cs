using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.Auth;
using Domain.Entities;

namespace Applications.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistUserEmailAsync(string email);
        Task<IResult> CreateUserAsync(User user);
        Task<IEnumerable<string>> GetUserRoleNameByUserId(Guid userId);
        Task<IDataResult<User>> GetUserByEmailAsyınc(string email);
    }
}