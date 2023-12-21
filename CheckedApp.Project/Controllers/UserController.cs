using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
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
         
    [HttpGet("UserData/{id}")]
        public async Task<IActionResult> GetUserData([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var userData = await _userService.GetUserDataDtoAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(userData);
        }

    [HttpGet("UserData/users")]
        public async Task<IActionResult> GetAllUsersData()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var usersDatas = await _userService.GetAllUsersDataDtoAsync();

            return usersDatas == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(usersDatas);
        }

    [HttpPost("UserData")]
        public async Task<IActionResult> AddUser(AddUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userService.AddUserAsync(dto);

            return Ok(new{ Message = "User created successfully" });
        }

        [HttpPut("UserData/{id}")]
        public async Task<IActionResult> EditUser([FromBody] AddUserDTO dto, [FromRoute] int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = await _userService.UpdateUser(dto);

            return isUpdated == false ? 
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) 
                : (Ok(new { Message = "Changes added successfully" }));
        }

    [HttpDelete("UserData/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isDeleted = await _userService.DeleteUserDataAsync(id);

            return isDeleted == false ? 
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "User successfully deleted" }));
        }    
    }
}
