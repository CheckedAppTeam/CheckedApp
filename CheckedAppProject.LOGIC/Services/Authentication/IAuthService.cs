using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshTokenAsync(RefreshTokenDTO refreshToken);
        Task<AuthResult> RegisterAsync(AddUserDTO addUserDto);
    }
}