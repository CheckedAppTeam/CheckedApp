using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemListRepository
    {
        Task<ItemList> CopyItemList(int itemListid, string userId);
        Task CreateItemList(ItemList itemList);
        Task<bool> DeleteAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<IEnumerable<ItemList>> GetAllByUserIdAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<IEnumerable<ItemList>> GetAllItemListsAsync();
        Task<IEnumerable<ItemList>> GetAllItemListsByCity(string city);
        Task<IEnumerable<ItemList>> GetAllItemListsByCityAndMonthAsync(string city, DateTime date);
        Task<ItemList> GetItemListAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<string> GetItemListNameByIdAsync(int itemListId);
        Task<bool> UpdateItemListAsync(ItemList itemList, int id);
    }
}