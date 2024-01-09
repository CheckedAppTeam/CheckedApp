using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.AutoMapperProfiles;


namespace CheckedAppProject.LOGIC.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
        public async Task<IEnumerable<Item>> GetAllItemDtoAsync()
        {
            var items = await _itemRepository.GetAllItemsAsync();

            //var itemsDtos = _mapper.Map<List<ItemDTO>>(items);

            return items;
        }
        public async Task<Item> GetItemById(int id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            return item;
        }
        public async Task AddItemAsync(ItemDTO dto)
        {
            var item = _mapper.Map<Item>(dto);
            await _itemRepository.AddItemAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int itemId)
        {
            return await _itemRepository.DeleteItemAsync(query => query.Where(u => u.ItemId == itemId));

        }

        public async Task<bool> EditItemAsync(ItemDTO dto, int itemId)
        {
            var item = _mapper.Map<Item>(dto);
            return await _itemRepository.EditItemAsync(item, itemId);
        }

        //public async Task<bool> ToggleItemStateAsync(int itemId, string itemState)
        //{

        //var userItem = _userItemContext.UserItems
        //    .FirstOrDefault(ui => ui.Item.ItemName == itemName);

        //if (userItem != null)
        //{
        //    switch (itemState.ToLower())
        //    {
        //        case "tobuy":
        //            userItem.ItemState = "ToBuy";
        //            break;
        //        case "topack":
        //            userItem.ItemState = "ToPack";
        //            break;
        //        default:
        //            _logger.LogInformation("Invalid itemState. Use 'ToBuy' or 'ToPack'.");
        //            break;
        //    }

        //    await _userItemContext.SaveChangesAsync();
        //}
        //else
        //{
        //    _logger.LogInformation("Item not found in the specified list");
        //}
        //}
    }
}
