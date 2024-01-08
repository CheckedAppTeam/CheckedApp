using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.Extensions.Logging;


namespace CheckedAppProject.LOGIC.Services
{
    public class ItemService : IItemService
    {
        private readonly ILogger<ItemService> _logger;
        private readonly UserItemContext _userItemContext;
        private readonly IMapper _mapper;

        public ItemService(UserItemContext userItemContext, IMapper mapper, ILogger<ItemService> logger)
        {
            _userItemContext = userItemContext ?? throw new ArgumentNullException(nameof(userItemContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddItemAsync(string name, string? company)
        {
            var newItem = new Item
            {
                ItemName = name,
                ItemCompany = company
            };

            try {
            _userItemContext.Items.Add(newItem);
            _logger.LogInformation($"added item:{name}");
            } catch (Exception ex) { 
                _logger.LogError(ex.ToString(),ex);
            }
            await _userItemContext.SaveChangesAsync();
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
                switch (itemState.ToLower())
                {
                    case "tobuy":
                        userItem.ItemState = "ToBuy";
                        break;
                    case "topack":
                        userItem.ItemState = "ToPack";
                        break;
                    default:
                        _logger.LogInformation("Invalid itemState. Use 'ToBuy' or 'ToPack'.");
                        break;
                }

                await _userItemContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("Item not found in the specified list");
            }
        }
    }
}
