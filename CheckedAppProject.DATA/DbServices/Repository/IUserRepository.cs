using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User userData);
        Task<bool> DeleteUserAsync(Func<IQueryable<User>, IQueryable<User>> customQuery);
        Task<bool> EditUserData(User userData, int userId);
        Task<IEnumerable<User>> GetAllUsersDataAsync();
        Task<User> GetUserAsync(Func<IQueryable<User>, IQueryable<User>> customQuery);
    }
}