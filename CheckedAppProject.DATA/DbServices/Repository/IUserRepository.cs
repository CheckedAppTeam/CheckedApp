using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserAccount userData);
        Task<bool> DeleteUserAsync(Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery);
        Task<bool> EditUserData(UserAccount userData, string userId);
        Task<IEnumerable<UserAccount>> GetAllUsersDataAsync();
        Task<UserAccount> GetUserAsync(Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery);
    }
}