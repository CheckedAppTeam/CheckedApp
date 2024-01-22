using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RegisterAsync(AddUserDTO addUserDto);
    }
}