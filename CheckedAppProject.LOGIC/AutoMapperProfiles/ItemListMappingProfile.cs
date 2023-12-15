
using AutoMapper;
using CheckedAppProject.DATA;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class ItemListMappingProfile : Profile
    {
        public ItemListMappingProfile()
        {
            CreateMap<ItemListTable, ItemListDTO>()
            .ForMember(dest => dest.ItemListId, opt => opt.MapFrom(src => src.ItemListTableId))
            .ForMember(dest => dest.ListName, opt => opt.MapFrom(src => src.ItemListName))
            .ForMember(dest => dest.TravelDestination, opt => opt.MapFrom(src => src.ItemListDestination))
            .ForMember(dest => dest.TravelDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.ItemListPublic));
        }
    }
}
