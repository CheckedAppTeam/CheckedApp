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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        //GET Single User data by Id
        [HttpGet("UserData/{id}")]
        public async Task<IActionResult> GetUserData([FromRoute] string id)
        {
            var userData = await _userService.GetUserDataDtoAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(userData);
        }

        //GET All Users data
        [HttpGet("UserData/AllUsers")]
        public async Task<IActionResult> GetAllUsersData()
        {
            var usersDatas = await _userService.GetAllUsersDataDtoAsync();

            return usersDatas == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(usersDatas);
        }

        //PUT Editing single user by ID
        [HttpPut("UserData/EditUser/{id}")]
        public async Task<IActionResult> EditUser([FromBody] UserUpdateDTO dto, [FromRoute] string id)
        {
            var isUpdated = await _userService.UpdateUser(dto, id);

            return isUpdated == false ?
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
        }

        //DELETE Deleting User by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpDelete("UserData/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var isDeleted = await _userService.DeleteUserDataAsync(id);

            return isDeleted == false ?
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "User successfully deleted" }));
        }
    }
}