using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Results.Implementations;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.UserRole;
using Applications.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.UserRoleRepository;

namespace Applications.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserRoleService(IUserRoleRepository userRoleRepository,IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddUserRoleAsync(UserRoleAddRequest request)
        {
            var userRole = _mapper.Map<UserRole>(request);
            await _userRoleRepository.AddAsync(userRole);
            if (request.UserId == Guid.Empty || request.RoleId == Guid.Empty)
            {
                return new ErrorResult("UserId or RoleId cannot be empty");
            }
            return new SuccessResult("User Role added successfully");
        }

    }
}