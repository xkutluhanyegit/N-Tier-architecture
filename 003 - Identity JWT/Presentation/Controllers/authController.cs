using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.Auth;
using Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IAuthService _authService;
        public authController(IUserService userService, IUserRoleService userRoleService, IAuthService authService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}