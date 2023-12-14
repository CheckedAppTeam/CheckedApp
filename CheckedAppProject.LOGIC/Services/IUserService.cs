using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserDTO dto);
        Task<UserDataDTO> GetUserDataDtoAsync(int userId);
    }
}