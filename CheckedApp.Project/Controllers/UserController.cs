using CheckedAppProject.DATA;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        
    
    [HttpGet("GetUserData/{id}")]
        public async Task<IActionResult> GetUserData([FromRoute] int id)
        {
            var userData = await _userService.GetUserDataDtoAsync(id);

            if (userData == null)
                return NotFound(new { ErrorCode = 404, Message = "User with this ID not found" });

            return Ok(userData);
        }


        //public void AddUser(UserDataDTO user) { }
        //public void EditUser(int id) { }
        //public void DeleteUser(int id) { }
        //public void LogInUser(string email,  string password) { }
        //public void LogOutUser() { }
    }
}
