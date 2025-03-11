using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.Common.Messages;
using Applications.Common.Results.Implementations;
using Applications.Common.Results.Interfaces;
using Applications.DTOs.Request.Auth;
using Applications.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.UserRepository;

namespace Applications.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResult> CreateUserAsync(User user)
        {
            var result = _userRepository.AddAsync(user);
            if (result == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
            
        }

        public async Task<bool> ExistUserEmailAsync(string email)
        {
            var entity = await _userRepository.GetAsync(x => x.Email == email);
            if (entity != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IDataResult<User>> GetUserByEmailAsyınc(string email)
        {
            var entity = await _userRepository.GetAsync(x => x.Email == email);
            if (entity == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(entity);
        }

        public async Task<IEnumerable<string>> GetUserRoleNameByUserId(Guid userId)
        {
            var result = await _userRepository.GetUserRoleNameByUserId(userId);
            if (!result.Any())
            {
                return new List<string> { "User role not found" };
            }
            return result.ToList();
        }
    }
}