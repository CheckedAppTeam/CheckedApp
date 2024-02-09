using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("UserId")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserItemController : ControllerBase
    {
        private readonly IUserItemService _userItemService;

        public UserItemController(IUserItemService userItemService)
        {
            _userItemService = userItemService;
        }

        // GET userItem by Id
        [HttpGet("GetUserItem/{id}")]
        public async Task<IActionResult> GetUserItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userData = await _userItemService.GetUserItemAsync(id);

            return userData == null ? (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" })) : Ok(userData);
        }

        // GET userItem by Id and state of the userItem
        [HttpGet("ByState/{state}/InItemList/{id}")]
        public async Task<IActionResult> GetAllUsersItemsByStateInItemList([FromRoute] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByState = await _userItemService.GetAllUserItemsByStateInItemListAsync(state, id);

            return usersItemsByState == null ?
                (NotFound(new { ErrorCode = 404, Message = "No list or items with this status found" })) : Ok(usersItemsByState);
        }

        //GET all userItems from choosen ItemList by itemList Id
        [HttpGet("ByListId/{id}")]
        public async Task<IActionResult> GetAllUsersItemsByListId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usersItemsByList = await _userItemService.GetAllUserItemsByListAsync(id);

            return usersItemsByList == null ?
                (NotFound(new { ErrorCode = 404, Message = "No items in this list found or such list not found" })) : Ok(usersItemsByList);
        }

        //POST new userItem to choosen list
        [HttpPost("AddItemToList")]
        public async Task<IActionResult> AddUserItem(AddUserItemDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _userItemService.AddUserItemAsync(dto);

            return Ok(new { Message = "UserItem created successfully" });
        }

        //PUT editing userItem when on list by userItem Id
        [HttpPut("EditItemOnList/{id}")]
        public async Task<IActionResult> EditUserItemStatus([FromBody] UserItemState state, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = await _userItemService.UpdateUserItemStateAsync(state, id);

            return isUpdated == false ?
                (NotFound(new { ErrorCode = 404, Message = "User item with this ID not found" }))
                : (Ok(new { Message = "Changes added successfully" }));
        }

        //DELETE remove userItem from list by userItem Id
        [HttpDelete("DeleteItemFromList/{id}")]
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
