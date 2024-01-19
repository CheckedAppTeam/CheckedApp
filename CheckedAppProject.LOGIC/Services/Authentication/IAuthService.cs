namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RegisterAsync(string email, string username, string password, int userAge, string userSurname, string userSex);
    }
}