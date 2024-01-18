namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string username, string password);
        Task<AuthResult> LoginAsync(string username, string password);
    }
}