using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task<UserDataDTO> GetUserDataDtoAsync(int userId);
    }
}