
using Applications.DTOs.Request.Auth;
using Applications.DTOs.Request.UserRole;
using Applications.DTOs.Response.Auth;
using Applications.Interfaces;
using Applications.Mappings.UserProfile;
using AutoMapper;
using CrossCutting.Validation;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Identity.Hashing;
using Infrastructure.Identity.Jwt;
using Infrastructure.Persistence.Repositories.UserRepository;

namespace Applications.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public AuthService(ITokenService tokenService, IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper,IUnitOfWork unitOfWork,IUserService userService,IRoleService roleService,IUserRoleService userRoleService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userService.GetUserByEmailAsyınc(request.Email);
            if (user.Data == null || !_passwordHasher.VerifyPasswordHash(request.Password, user.Data.PasswordHash, user.Data.PasswordSalt))
            {
                throw new Exception("Error Email or password");
            }

            var userRoles = await _userService.GetUserRoleNameByUserId(user.Data.Id);
            var token = _tokenService.GenerateToken(user.Data.Id, user.Data.Email, userRoles);
            return new AuthResponse
            {
                Token = token,
                Email = user.Data.Email,
                Roles = userRoles
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            
            var userExist = await _userService.ExistUserEmailAsync(request.Email);
            if (userExist)
            {
                throw new Exception("User already exist");
            }

            var user = _mapper.Map<User>(request);
            _passwordHasher.CreatePasswordHash(request.Password, out byte[] _passwordHash, out byte[] _passwordSalt);
            user.PasswordHash = _passwordHash;
            user.PasswordSalt = _passwordSalt;

            await _userService.CreateUserAsync(user);

            var userRoleId = await _roleService.GetByRoleIdAsync(RoleName.User.ToString());

            await _userRoleService.AddUserRoleAsync(new UserRoleAddRequest
            {
                UserId = user.Id,
                RoleId = userRoleId
            });

            await _unitOfWork.CommitTransactionAsync();

            var userRoleList = await _userService.GetUserRoleNameByUserId(user.Id);
            
            


        
            var token = _tokenService.GenerateToken(user.Id, user.Email, userRoleList);
            
            

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Roles = userRoleList
            };

        }
    }
}