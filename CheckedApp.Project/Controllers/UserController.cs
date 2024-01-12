using AutoMapper;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUserService userService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
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

            AppUser appUser = _mapper.Map<AppUser>(dto);
            IdentityResult result = await _userManager.CreateAsync(appUser, dto.Password);

            //TUTAJ MUSZĘ PRZYPISAĆ ID APPUSERA DO USERACCOUNT USERID
            //NIE WIDZI TUTAJ TEGO ID W DTO

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            await _userService.AddUserAsync(dto);

            return Ok(new{ Message = "User created successfully" });
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
