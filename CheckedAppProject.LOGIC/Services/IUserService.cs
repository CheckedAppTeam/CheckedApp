using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserDTO dto);
        Task<UserDataDTO> GetUserDataDtoAsync(int userId);
        Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync();
        Task<bool> DeleteUserDataDtoAsync(int userId);
        Task<bool> UpdateUser(int userId, AddUserDTO dto);
    }
}