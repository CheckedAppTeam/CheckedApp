using CheckedAppProject.DATA;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    public class UserController : ControllerBase
    {
        public void AddUser(UserDataDTO user) { }
        public User GetUserData(int id) {  return null; }
        public void EditUser(int id) { }
        public void DeleteUser(int id) { }
        public void LogInUser(string email,  string password) { }
        public void LogOutUser() { }
    }
}
