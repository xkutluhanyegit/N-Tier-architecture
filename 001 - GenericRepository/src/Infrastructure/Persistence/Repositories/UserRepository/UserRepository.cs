using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TrinkappContext context) : base(context)
        {
        }

        public Task<List<string>> GetUserRoleNameByUserId(Guid userId)
        {
            var result = from users in _context.Users
                         join userRoles in _context.UserRoles on users.Id equals userRoles.UserId
                         join roles in _context.Roles on userRoles.RoleId equals roles.Id
                         where users.Id == userId
                         select roles.Name;
            return result.ToListAsync();
        }
    }
}