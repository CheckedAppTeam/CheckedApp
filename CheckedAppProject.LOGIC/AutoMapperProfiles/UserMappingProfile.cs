using AutoMapper;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;


namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserAccount, UserDataDTO>()
                .ForMember(dest => dest.OwnItemList, opt => opt.MapFrom(src => src.ItemList));

            CreateMap<AddUserDTO, UserAccount>()
                .ForMember(dest => dest.UserAccountName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<AddUserDTO, AppUser>()
                .ForMember(dest => dest.UserName, opt  => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserUpdateDTO, UserAccount>()
            .ForAllMembers(opt => opt.UseDestinationValue());
        }
    }
}
