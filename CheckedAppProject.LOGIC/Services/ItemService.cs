using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.Extensions.Logging;


namespace CheckedAppProject.LOGIC.Services
{
    public class ItemService : IItemService
    {
        private readonly ILogger<ItemService> _logger;
        private readonly UserItemContext _userItemContext;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemService(UserItemContext userItemContext, IMapper mapper, ILogger<ItemService> logger, IItemRepository itemRepository)
        {
            _userItemContext = userItemContext ?? throw new ArgumentNullException(nameof(userItemContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task AddItemAsync(ItemDTO dto)
        {
            var item = _mapper.Map<Item>(dto);
            await _itemRepository.AddItemAsync(item);
        }

        public async Task DeleteItemAsync(string itemName, int itemListId)
        {
            var userItem = _userItemContext.UserItems
                .FirstOrDefault(ui => ui.Item.ItemName == itemName && ui.ItemListId == itemListId);

            if (userItem != null)
            {
                _userItemContext.UserItems.Remove(userItem);
                await _userItemContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Item not found in the specified list");
            }
        }

        public async Task EditNameAsync(string itemName)
        {
            var userItem = _userItemContext.UserItems
                .FirstOrDefault(ui => ui.Item.ItemName == itemName);

            if (userItem != null)
            {
                userItem.Item.ItemName = $"{itemName}";
                await _userItemContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Item not found in the specified list");
            }
        }

        public async Task ToggleItemStateAsync(string itemName, string itemState)
        {
            var userItem = _userItemContext.UserItems
                .FirstOrDefault(ui => ui.Item.ItemName == itemName);

            if (userItem != null)
            {
                

                await _userItemContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Item not found in the specified list");
            }
        }
    }
}
