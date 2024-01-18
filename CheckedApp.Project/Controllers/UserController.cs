using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("User")]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private UserManager<AppUser> _userManager;
        private UserItemContext _userItemContext;


        public UserController(IUserService userService, UserManager<AppUser> userManager, UserItemContext userItemContext)
        {
            _userService = userService;
            _userManager = userManager;
            _userItemContext = userItemContext;
        }

        [HttpGet("UserData/{id}")]
        public async Task<IActionResult> GetUserData([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userData = await _userService.GetUserDataDtoAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(userData);
        }

        [HttpGet("UserData/AllUsers")]
        public async Task<IActionResult> GetAllUsersData()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersDatas = await _userService.GetAllUsersDataDtoAsync();

            return usersDatas == null ? (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" })) : Ok(usersDatas);
        }

        [HttpPost("UserData/AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var identityUser = new AppUser()
            {
                Email = dto.UserEmail,
                UserName = dto.UserName,
                UserSurname = dto.UserSurname,
                UserAge = dto.UserAge,
                UserSex = dto.UserSex,
            };
            var result = await _userManager.CreateAsync(identityUser, dto.Password);
            if (result.Succeeded)
            {
                await _userItemContext.SaveChangesAsync();
                return Ok("Index");
            }
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }


            //await _userService.AddUserAsync(dto);

            //return Ok(new{ Message = "User created successfully" });
        }

        [HttpPut("UserData/EditUser/{id}")]
        public async Task<IActionResult> EditUser([FromBody] UserUpdateDTO dto, [FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = await _userService.UpdateUser(dto, id);

            return isUpdated == false ?
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
        }

        [HttpDelete("UserData/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isDeleted = await _userService.DeleteUserDataAsync(id);

            return isDeleted == false ?
                (NotFound(new { ErrorCode = 404, Message = "User with this ID not found" }))
                : (Ok(new { Message = "User successfully deleted" }));
        }
    }
}