using AutoMapper;
using CheckedAppProject.DATA;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserTable, UserDataDTO>();
                //.ForMember(dest => dest.OwnItemList, opt => opt.MapFrom(src => src.OwnItemList));

        }// pytanie o zmapowanie listy list która jest w User??
    }
}
