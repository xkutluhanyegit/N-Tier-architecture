using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.Role;
using Applications.Services;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappings.RoleProfile
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<Role,RoleAddRequest>();
            CreateMap<RoleAddRequest,Role>();

        }
    }
}