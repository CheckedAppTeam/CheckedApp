using CheckedAppProject.LOGIC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserService
    {
        private bool AuthenticateUser(string Email, string? Pasword)
        {
            return true;
        }
        private void CreateUser(UserInputData data) { }

        private UserDataDTO GetLoggedUserData(string Email,string Password)
        {
            return new UserDataDTO();
        }
        private void EditUserData(int UserId)
        {

        }
        private void LogOutUser() { }
        private void LogInUser() { }
        private bool CheckIfUserIsLogged() 
        {
            return true;
        }
    }
}
