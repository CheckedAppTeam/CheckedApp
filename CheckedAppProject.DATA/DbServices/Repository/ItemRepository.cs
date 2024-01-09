using Castle.Core.Logging;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class ItemRepository : IItemRepository
    {
        private UserItemContext _userItemContext;


        public ItemRepository(UserItemContext userItemContext)
        {
            _userItemContext = userItemContext;
        }

        public async Task AddItemAsync(Item item)
        {
            try
            {
                _userItemContext.Items.Add(item);
                await _userItemContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditItemAsync(string itemName, string newItemName, string newItemCompany = null)
        {
            var itemToEdit = _userItemContext.Items.FirstOrDefault(item => item.ItemName == itemName);

            if (itemToEdit != null)
            {
                itemToEdit.ItemName = newItemName;
                itemToEdit.ItemCompany = newItemCompany;
            }
            else
            {
                throw new ArgumentException("Item not found");
            }

            await Task.CompletedTask;
        }

        public async Task<List<Item>> GetAllItemListAsync()
        {
            return null;
        }

        public async Task<Item> GetItemAsync(string itemName)
        {
            return await Task.FromResult(_userItemContext.Items.FirstOrDefault(item => item.ItemName == itemName));
        }
    }
}
