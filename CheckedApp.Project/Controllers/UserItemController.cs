using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserItemController : ControllerBase
    {
        private readonly IUserItemService _userItemService;

        public UserItemController(IUserItemService useritemService)
        {
            _userItemService = userItemService;
        }

        [HttpGet("UserItem/{id}")]
        public async Task<IActionResult> GetUserItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userData = await _userItemService.GetUserItemByIdAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" })) : Ok(userData);
        }

        [HttpGet("UserItem/ByDestination")]
        public async Task<IActionResult> GetAllUsersItemsByDestination([FromBody] string cityName)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByDestination = await _userItemService.GetAllUserItemsByDestinationAsync();

            return usersItemsByDestination == null ? 
                (NotFound(new { ErrorCode = 404, Message = "Items in this destination or such destination not found" })) : Ok(usersItemsByDestination);
        }

        [HttpGet("UserItem/ByStateInItemList={id}")]
        public async Task<IActionResult> GetAllUsersItemsByStateInItemList([FromBody] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByState = await _userItemService.GetAllUserItemsByStateInItemListAsync(state, id);

            return usersItemsByState == null ?
                (NotFound(new { ErrorCode = 404, Message = "No list or items with this status found" })) : Ok(usersItemsByState);
        }
        [HttpGet("UserItem/ByListId={id}")]
        public async Task<IActionResult> GetAllUsersItemsByListId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByList = await _userItemService.GetAllUserItemsByListAsync(id);

            return usersItemsByList == null ?
                (NotFound(new { ErrorCode = 404, Message = "No items in this list found or such list not found" })) : Ok(usersItemsByList);
        }

        [HttpPost("UserItem")]
        public async Task<IActionResult> AddUserItem(UserItemDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userItemService.AddUserItemAsync(dto);

            return Ok(new { Message = "UserItem created successfully" });
        }

        [HttpPut("UserData/{id}")]
        public async Task<IActionResult> EditUserItemStatus([FromBody] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = await _userItemService.UpdateUserItemStateAsync(state, id);

            return isUpdated == false ?
                (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
        }

        [HttpDelete("UserData/{id}")]
        public async Task<IActionResult> DeleteUserItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isDeleted = await _userItemService.DeleteUserItemAsync(id);

            return isDeleted == false ?
                (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" }))
                : (Ok(new { Message = "User successfully deleted" }));
        }
    }
}
