using AutoMapper;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;


namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDataDTO>()
                .ForMember(dest => dest.OwnItemList, opt => opt.MapFrom(src => src.ItemList))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AddUserDTO, User>();


            CreateMap<UserUpdateDTO, User>()
            .ForAllMembers(opt => opt.UseDestinationValue());
        }
    }
}
