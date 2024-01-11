using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserItemContext _userItemContext;

        public UserRepository(UserItemContext userItemContext)
        {
            _userItemContext = userItemContext;
        }

        public async Task<UserAccount?> GetUserAsync(Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery)
        {
            var query = _userItemContext.UsersApp.AsQueryable();

            query = customQuery(query);

            return await query
                .Include(u => u.ItemList)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<UserAccount>> GetAllUsersDataAsync()
        {
            var users = await _userItemContext
                .UsersApp
                .Include(e => e.ItemList)
                .ToListAsync();

            return users;
        }
        public async Task<bool> DeleteUserAsync(Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery)
        {
            var query = _userItemContext.UsersApp.AsQueryable();

            query = customQuery(query);

            var userToDelete = await query.FirstOrDefaultAsync();

            if (userToDelete != null)
            {
                _userItemContext.UsersApp.Remove(userToDelete);
                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> EditUserData(UserAccount userData, string userId)
        {
            var dbUser = await _userItemContext.UsersApp
               .FirstOrDefaultAsync(u => u.AppUserId == userId);

            if (dbUser != null)
            {
                dbUser.UserAccountName = userData.UserAccountName ?? dbUser.UserAccountName; // a co ze zmianą Name w Identity??
                dbUser.UserSurname = userData.UserSurname ?? dbUser.UserSurname;
                dbUser.UserAge = userData.UserAge != 0 ? userData.UserAge : dbUser.UserAge;

                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task AddUserAsync(UserAccount userData)
        {
            try
            {
                _userItemContext.UsersApp.Add(userData);
                await _userItemContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
