using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        //GET Single User data by Id
        [HttpGet("UserData/{id}")]
        public async Task<IActionResult> GetUserData([FromRoute] string id)
        {
            try
            {
            var userData = await _userService.GetUserDataDtoAsync(id);

            return userData == null
                ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : Ok(userData);
            }catch (Exception ex){
                _logger.LogError(ex, $"An error occurred while getting data of user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        } 

        //GET All Users data
        [HttpGet("UserData/AllUsers")]
        public async Task<IActionResult> GetAllUsersData()
        {
            try
            {
            var usersDatas = await _userService.GetAllUsersDataDtoAsync();

            return usersDatas == null
                ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : Ok(usersDatas);
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting all users");
                return StatusCode(500, "Internal server error");
            }
        }

        //PUT Editing single user by ID
        [HttpPut("UserData/EditUser/{id}")]
        public async Task<IActionResult> EditUser([FromBody] UserUpdateDTO dto,[FromRoute] string id)
        {
            try
            {
            var isUpdated = await _userService.UpdateUser(dto, id);

            return isUpdated == false
                ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while editing user with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        //DELETE Deleting User by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpDelete("UserData/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
            var isDeleted = await _userService.DeleteUserDataAsync(id);

            return isDeleted == false
                ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "User successfully deleted" }));
            }catch(Exception ex) {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {id}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
