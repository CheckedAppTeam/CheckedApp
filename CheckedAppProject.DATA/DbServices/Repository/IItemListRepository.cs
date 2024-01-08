using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemListRepository
    {
        Task CreateItemList(ItemList itemList);
        Task<bool> DeleteAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<IEnumerable<ItemList>> GetAllByUserIdAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<IEnumerable<ItemList>> GetAllItemListsAsync();
        Task<ItemList> GetItemListAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<bool> UpdateItemListAsync(ItemList itemList, int id);
        Task<ItemList> CopyItemList(int itemListid, int userId);
        Task<IEnumerable<ItemList>> GetAllItemListsByCityAndMonthAsync(string city, DateTime date);
    }
}