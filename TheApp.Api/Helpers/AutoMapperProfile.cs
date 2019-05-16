using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheApp.Api.Dtos;
using TheApp.Api.Entities;

namespace TheApp.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTOOut>();
            CreateMap<UserDTOIn, User>();
            CreateMap<UserDTOIn, DBUser>();
        }
    }
}
