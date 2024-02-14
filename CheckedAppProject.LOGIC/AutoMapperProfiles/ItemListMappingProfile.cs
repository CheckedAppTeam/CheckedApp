using AutoMapper;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.AutoMapperProfiles
{
    public class ItemListMappingProfile : Profile
    {
        private readonly IItemRepository _itemRepository;
        public ItemListMappingProfile(IItemRepository itemRepository)
        {
        _itemRepository = itemRepository;
            
        }

        public ItemListMappingProfile()
        {

            CreateMap<ItemList, ItemListDTO>()
            .ForMember(dest => dest.ItemListId, opt => opt.MapFrom(src => src.ItemListId))
            .ForMember(dest => dest.ListName, opt => opt.MapFrom(src => src.ItemListName))
            .ForMember(dest => dest.TravelDestination, opt => opt.MapFrom(src => src.ItemListDestination))
            .ForMember(dest => dest.TravelDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.ItemListPublic));

            CreateMap<UserItem, ItemDTO>();
            CreateMap<ItemDTO, UserItem>();
            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();

            CreateMap<GetItemDTO, Item>();
            CreateMap<Item, GetItemDTO>();

            CreateMap<EditItemDTO, Item>();
            CreateMap<Item, EditItemDTO>();

            CreateMap<NewItemDTO, Item>();

            CreateMap<UserItem, UserItemDTO>()
            .ForMember(dest => dest.UserItemName, opt => opt.MapFrom(src => _itemRepository.GetItemNameByIdAsync(src.ItemId)))
            .ForMember(dest => dest.ItemState, opt => opt.MapFrom(src => src.ItemState))
            .ForMember(dest => dest.UserItemListName, opt => opt.MapFrom(src => src.ItemList.ItemListName));

            CreateMap<UserItemDTO, UserItem>();
            CreateMap<AddUserItemDTO, UserItem>();
            CreateMap<UserItem, AddUserItemDTO>();

            CreateMap<CreateItemListDTO, ItemList>()
             .ForAllMembers(opt => opt.UseDestinationValue());

            CreateMap<ItemListDTO, ItemList>();
            CreateMap<ItemList, CreateItemListDTO>();
            CreateMap<UpdateItemListDTO, ItemList>();
            CreateMap<ItemList, UpdateItemListDTO>();
        }
    }
}
