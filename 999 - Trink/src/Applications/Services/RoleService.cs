using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Messages;
using Applications.Common.Results.Implementations;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.Role;
using Applications.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.RoleRepository;

namespace Applications.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddRoleAsync(RoleAddRequest roleAddRequest)
        {
            var entity = await _roleRepository.GetAsync(x => x.Name == roleAddRequest.Name);
            if (entity != null)
            {
                return new ErrorResult(Messages.RoleAlreadyExists);
            }
            var role = _mapper.Map<Role>(roleAddRequest);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangeAsync();
            return new SuccessResult(Messages.AddRole);
        }

        public async Task<IDataResult<IEnumerable<Role>>> GetAllRolesAsync()
        {
            var entity = await _roleRepository.GetAllAsync();
            if (entity == null)
            {
                return new ErrorDataResult<IEnumerable<Role>>(Messages.RolesNotFound);
            }
            return new SuccessDataResult<IEnumerable<Role>>(entity,Messages.ListRoles);
        }

        public async Task<Guid> GetByRoleIdAsync(string roleName)
        {
            var entity = await _roleRepository.GetAsync(x => x.Name == roleName);
            if (entity == null)
            {
                throw new Exception(Messages.RoleNotFound);
            }
            return entity.Id;
        }
    }
}