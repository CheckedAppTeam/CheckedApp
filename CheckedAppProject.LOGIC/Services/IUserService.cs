using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task<bool> DeleteUserDataAsync(string userId);
        Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync();
        Task<UserDataDTO> GetUserDataDtoAsync(string id);
        Task<bool> UpdateUser(UserUpdateDTO dto, string userId);
    }
}