using AutoMapper;
using CheckedAppProject.DATA;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;


namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserTable, UserDataDTO>()
                .ForMember(dest => dest.OwnItemList, opt => opt.MapFrom(src => src.ItemListTable))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserTableId));

            CreateMap<AddUserDTO, UserTable>();
        }
    }
}
