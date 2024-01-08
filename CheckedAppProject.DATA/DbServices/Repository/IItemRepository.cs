using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task EditItemAsync(string itemName, string newItemName, string newItemCompany = null);
        Task<List<Item>> GetAllItemListAsync();
        Task<Item> GetItemAsync(string itemName);
    }
}