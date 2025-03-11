using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories.GenericRepository;

namespace Infrastructure.Persistence.Repositories.UserRoleRepository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(TrinkappContext context) : base(context)
        {
        }
    }
}