using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        bool AuthenticateUser(string Email, string? Pasword);
        bool CheckIfUserIsLogged();
        void CreateUser(UserInputData data);
        void EditUserData(int UserId);
        UserDataDTO GetLoggedUserData(string Email, string Password);
        void LogInUser();
        void LogOutUser();
    }
}