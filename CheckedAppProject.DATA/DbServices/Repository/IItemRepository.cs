using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemListAsync();
        Task<Item> GetItemAsync(string itemName);
        Task AddItemAsync(string itemName, string itemCompany = null);
        Task EditItemAsync(string itemName, string newItemName, string newItemCompany = null);
    }

}