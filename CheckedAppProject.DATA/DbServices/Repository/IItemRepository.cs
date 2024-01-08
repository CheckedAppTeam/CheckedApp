using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task<bool> DeleteItemAsync(Func<IQueryable<Item>, IQueryable<Item>> customQuery);
        Task<bool> EditItemAsync(Item itemData, int itemId);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemAsync(Func<IQueryable<Item>, IQueryable<Item>> customQuery);
    }
}