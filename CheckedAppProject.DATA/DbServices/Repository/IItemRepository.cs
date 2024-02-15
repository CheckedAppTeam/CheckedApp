using CheckedAppProject.DATA.Entities;
using CheckedAppProject.DATA.Models;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task<bool> DeleteItemAsync(Func<IQueryable<Item>, IQueryable<Item>> customQuery);
        Task<bool> EditItemAsync(Item itemData, int itemId);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<(IEnumerable<Item>, int, int, int)> GetAllItemsAsyncPages(ItemsQuery query);
        Task<Item> GetItemByIdAsync(int itemId);
        Task<Item> GetItemByNameAsync(string name);
        Task<string> GetItemNameByIdAsync(int itemId);
    }
}