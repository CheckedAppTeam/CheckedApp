using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(AppUser userData);
        Task<bool> DeleteUserAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery);
        Task<bool> EditUserData(AppUser userData, string userId);
        Task<IEnumerable<AppUser>> GetAllUsersDataAsync();
        Task<AppUser> GetUserAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery);
    }
}