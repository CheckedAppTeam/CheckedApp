using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class UserItemRepository : IUserItemRepository
    {
        private UserItemContext _userItemContext;

        public UserItemRepository(UserItemContext userItemContext)
        {
            _userItemContext = userItemContext;
        }

        public async Task<UserItem?> GetUserItemRepositoryAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery)
        {
            var query = _userItemContext.UserItems.AsQueryable();

            query = customQuery(query);

            return await query
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserItem?>> GetAllUserItemAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery)
        {
            var query = _userItemContext.UserItems.AsQueryable();

            query = customQuery(query);

            return await query
                .ToListAsync();
        }

        public async Task AddUserItemAsync(UserItem userData)
        {
            try
            {
                _userItemContext.UserItems.Add(userData);
                await _userItemContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EditUserItemData(UserItemState state, int userItemId)
        {
            var dbUserItem = await _userItemContext.UserItems
               .FirstOrDefaultAsync(u => u.UserItemId == userItemId);

            if (dbUserItem != null)
            {
                dbUserItem.ItemState = state;
                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserItemAsync(Func<IQueryable<UserItem>, IQueryable<UserItem>> customQuery)
        {
            var query = _userItemContext.UserItems.AsQueryable();

            query = customQuery(query);

            var userItemToDelete = await query.FirstOrDefaultAsync();

            if (userItemToDelete != null)
            {
                _userItemContext.UserItems.Remove(userItemToDelete);
                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<UserItem?>> GetAllUserItemFromListAsync(int itemListId)
        {
            List<UserItem> userItemList = await _userItemContext.UserItems
                .Where(ui => ui.ItemListId == itemListId).ToListAsync();

            return userItemList;
        }

    }
}