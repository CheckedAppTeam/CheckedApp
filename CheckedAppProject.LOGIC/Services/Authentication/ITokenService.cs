using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}