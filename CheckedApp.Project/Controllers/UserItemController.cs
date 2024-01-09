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

        public UserItemController(IUserItemService userItemService)
        {
            _userItemService = userItemService;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userData = await _userItemService.GetUserItemAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" })) : Ok(userData);
        }

        [HttpGet("/ByState{state}InItemList{id}")]
        public async Task<IActionResult> GetAllUsersItemsByStateInItemList([FromRoute] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByState = await _userItemService.GetAllUserItemsByStateInItemListAsync(state, id);

            return usersItemsByState == null ?
                (NotFound(new { ErrorCode = 404, Message = "No list or items with this status found" })) : Ok(usersItemsByState);
        }
        [HttpGet("/ByListId={id}")]
        public async Task<IActionResult> GetAllUsersItemsByListId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByList = await _userItemService.GetAllUserItemsByListAsync(id);

            return usersItemsByList == null ?
                (NotFound(new { ErrorCode = 404, Message = "No items in this list found or such list not found" })) : Ok(usersItemsByList);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUserItem(AddUserItemDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userItemService.AddUserItemAsync(dto);

            return Ok(new { Message = "UserItem created successfully" });
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> EditUserItemStatus([FromBody] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = await _userItemService.UpdateUserItemStateAsync(state, id);

            return isUpdated == false ?
                (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
        }

        [HttpDelete("/{id}")]
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
