using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.Role;
using Applications.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class roleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public roleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet("getall-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] RoleAddRequest roleAddRequest)
        {
            var result = await _roleService.AddRoleAsync(roleAddRequest);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}