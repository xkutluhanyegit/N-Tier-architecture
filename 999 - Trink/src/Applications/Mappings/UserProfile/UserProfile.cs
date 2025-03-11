using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.Auth;
using AutoMapper;
using Domain.Entities;

namespace Applications.Mappings.UserProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User,RegisterRequest>();
            CreateMap<RegisterRequest,User>();
            CreateMap<User,LoginRequest>();
            CreateMap<LoginRequest,User>();
        }
    }
}