
using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.Services.Logger;

namespace CheckedAppProject.LOGIC.Services
{
    public class ItemService : IItemService
    {
        private readonly UserItemContext _userItemContext;
        private readonly IMapper _mapper;
        private readonly IAppLogger _logger;
        public ItemService(UserItemContext userItemContext, IMapper mapper, IAppLogger logger)
        {
            _userItemContext = userItemContext;
            _mapper = mapper;
            _logger = logger;
        }
        public void AddItem(string name, string? company)
        {
            var newItem = new Item
            {
                ItemName = name,
                ItemCompany = company
            };

            _userItemContext.Items.Add(newItem);
            _userItemContext.SaveChanges();
        }

        public void DeleteItem(string itemName, int itemListId)
        {
            var userItem = _userItemContext.UserItems
                .Where(ui => ui.Item.ItemName == itemName && ui.ItemListId == itemListId)
                .FirstOrDefault();

            if (userItem != null)
            {
                _userItemContext.UserItems.Remove(userItem);
                _userItemContext.SaveChanges();
            }
            else
            {
                _logger.LogException(new ArgumentException(), "Item not found in the specified list");
            }
        }

        public void EditName(string itemName)
        {
            var userItem = _userItemContext.UserItems
                .Where(ui => ui.Item.ItemName == itemName)
                .FirstOrDefault();

            if (userItem != null)
            {
                userItem.Item.ItemName = $"{itemName}";
                _userItemContext.SaveChanges();
            }
            else
            { 
                _logger.LogException(new ArgumentException(), "Item not found ");
            }
        }

        public void ToggleItemState(string itemName, string itemState)
        {
            var userItem = _userItemContext.UserItems
                .Where(ui => ui.Item.ItemName == itemName)
                .FirstOrDefault();

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
                        _logger.LogException(new ArgumentException(), "Invalid itemState. Use 'ToBuy' or 'ToPack'.");
                        break;                
                }

                _userItemContext.SaveChanges();
            }
            else
            {
                _logger.LogException(new ArgumentException(), "Item not found ");
            }
        }
    }
}