using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserDTO dto);
        Task<bool> DeleteUserDataAsync(int userId);
        Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync();
        Task<UserDataDTO> GetUserDataDtoAsync(int userId);
        Task<bool> UpdateUser(AddUserDTO dto);
    }
}