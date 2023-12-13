using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserService : IUserService
    {
        public bool AuthenticateUser(string Email, string? Pasword)
        {
            return true;
        }
        public void CreateUser(UserInputData data) { }

        public UserDataDTO GetLoggedUserData(string Email, string Password)
        {
            return new UserDataDTO();
        }
        public void EditUserData(int UserId)
        {

        }
        public void LogOutUser() { }
        public void LogInUser() { }
        public bool CheckIfUserIsLogged()
        {
            return true;
        }
    }
}
