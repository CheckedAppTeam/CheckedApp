using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IUserItemRepository
    {
        Task AddUserItemAsync(UserItem userData);
        Task<bool> DeleteUserItemAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery);
        Task<bool> EditUserItemData(UserItemState state, int userItemId);
        Task<List<UserItem>> GetAllUserItemAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery);
        Task<UserItem> GetUserItemRepositoryAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery);
        Task<List<UserItem?>> GetAllUserItemFromListAsync(int itemListId);
    }
}