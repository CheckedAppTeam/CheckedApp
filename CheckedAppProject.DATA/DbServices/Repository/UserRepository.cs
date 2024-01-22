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

        public async Task<AppUser?> GetUserAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery)
        {
            var query = _userItemContext.Users.AsQueryable();

            query = customQuery(query);

            return await query
                .Include(u => u.ItemList)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<AppUser>> GetAllUsersDataAsync()
        {
            var users = await _userItemContext
                .Users
                .Include(e => e.ItemList)
                .ToListAsync();

            return users;
        }
        public async Task<bool> DeleteUserAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery)
        {
            var query = _userItemContext.Users.AsQueryable();

            query = customQuery(query);

            var userToDelete = await query.FirstOrDefaultAsync();

            if (userToDelete != null)
            {
                _userItemContext.Users.Remove(userToDelete);
                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> EditUserData(AppUser userData, string userId)
        {
            var dbUser = await _userItemContext.Users
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (dbUser != null)
            {
                dbUser.UserName = userData.UserName ?? dbUser.UserName;
                dbUser.UserSurname = userData.UserSurname ?? dbUser.UserSurname;
                dbUser.PasswordHash = userData.PasswordHash ?? dbUser.PasswordHash;
                dbUser.UserAge = userData.UserAge != 0 ? userData.UserAge : dbUser.UserAge;

                await _userItemContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}