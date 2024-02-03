using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class ItemRepository : IItemRepository
    {
        private UserItemContext _userItemContext;
        private readonly ILogger<ItemRepository> _logger;
        public ItemRepository(UserItemContext userItemContext, ILogger<ItemRepository> logger)
        {
            _userItemContext = userItemContext;
            _logger = logger;
        }

        public async Task AddItemAsync(Item item)
        {
            try
            {
                _logger.LogInformation($"Added item: {item.ItemName} [{item.ItemCompany}]");

                _userItemContext.Items.Add(item);
                await _userItemContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding item to the database");
                throw;
            }
        }

        public async Task<bool> EditItemAsync(Item itemData, int itemId)
        {
            var dbItem = await _userItemContext.Items
                 .FirstOrDefaultAsync(i => i.ItemId == itemId);
            var previousName = dbItem.ItemName;
            var previousCompany = dbItem.ItemCompany;

            if (dbItem != null)
            {
                
                dbItem.ItemName = itemData.ItemName ?? dbItem.ItemName;
                dbItem.ItemCompany = itemData.ItemCompany ?? dbItem.ItemCompany;

                await _userItemContext.SaveChangesAsync();
                _logger.LogInformation($"Item changed: {previousName} {previousCompany} is now {itemData.ItemName} {itemData.ItemCompany}");
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            var items = await _userItemContext.Items.ToListAsync();

            return items;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var item = await _userItemContext.Items
                .Where(i => i.ItemId == itemId)
                .FirstOrDefaultAsync();

            return item;
        }
        public async Task<string> GetItemNameByIdAsync(int itemId)
        {
            var item = await _userItemContext.Items
                .Where(i => i.ItemId == itemId)
                .FirstOrDefaultAsync();
            var itemName = (item != null) ? item.ItemName : "DefaultName";
            //var itemName = item.ItemName;

            return itemName;
        }
        public async Task<Item> GetItemByNameAsync(string name)
        {
            var item = await _userItemContext.Items
                .Where(i => i.ItemName == name)
                .OrderBy(i => i.ItemId)
                .LastOrDefaultAsync();

            return item;
        }
        
        public async Task<bool> DeleteItemAsync(Func<IQueryable<Item>, IQueryable<Item>> customQuery)
        {
            var query = _userItemContext.Items.AsQueryable();
            query = customQuery(query);

            var itemToDelete = await query.FirstOrDefaultAsync();

            if (itemToDelete != null)
            {
                _userItemContext.Items.Remove(itemToDelete);
                await _userItemContext.SaveChangesAsync();
                _logger.LogInformation($"Deleted {itemToDelete.ItemName} with id:{itemToDelete.ItemId}");
                return true;
            }
            return false;
        }
    }
}
