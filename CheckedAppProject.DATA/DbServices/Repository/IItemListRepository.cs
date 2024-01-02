using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IItemListRepository
    {
        Task CreateItemList(ItemList itemList);
        Task<bool> DeleteAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
        Task<IEnumerable<User>> GetAllByUserIdAsync(Func<IQueryable<User>, IQueryable<User>> customQuery);
        Task<IEnumerable<ItemList>> GetAllItemListsAsync();
        Task<ItemList> GetItemListAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery);
    }
}