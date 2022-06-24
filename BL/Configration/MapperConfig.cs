using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Dto;
using Dll;

namespace BL.Configration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<ApplicationUsersIdentity, loginViewModel>().ReverseMap();
            CreateMap<ApplicationUsersIdentity, RegisterViewModel>().ReverseMap();

        }
    }
}
