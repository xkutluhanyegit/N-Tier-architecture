using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.UserRole;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappings.UserRoleProfile
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRoleAddRequest, UserRole>();
            CreateMap<UserRole, UserRoleAddRequest>();
        }
    }
}