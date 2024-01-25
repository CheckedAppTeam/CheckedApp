using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}