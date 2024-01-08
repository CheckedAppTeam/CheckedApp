using Castle.Core.Logging;
using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class ItemRepository : IItemRepository
    {
        private List<Item> Items { get; set; }
        
        public ItemRepository()
        {
            Items = new List<Item>();
        }
        public async Task AddItemAsync(string itemName, string itemCompany = null)
        {
            var newItem = new Item
            {
                ItemName = itemName,
                ItemCompany = itemCompany
            };

            Items.Add(newItem);
            await Task.CompletedTask;
        }

        public async Task EditItemAsync(string itemName, string newItemName, string newItemCompany = null)
        {
            var itemToEdit = Items.FirstOrDefault(item => item.ItemName == itemName);

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
            return await Task.FromResult(Items);
        }

        public async Task<Item> GetItemAsync(string itemName)
        {
            return await Task.FromResult(Items.FirstOrDefault(item => item.ItemName == itemName));
        }
    }
}
