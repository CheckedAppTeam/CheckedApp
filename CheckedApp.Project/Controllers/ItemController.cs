using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CheckedAppProject.DATA.Models;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("Item")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController( IItemService itemService)
        {
            _itemService = itemService;
        }

        //GET filtered items, one item - depending on the function provided - delegate
        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAllItemsPages([FromQuery]ItemsQuery query)
        {
           var items = await _itemService.GetAllItemDtoAsyncPages(query);
            return items == null ? NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }) : Ok(items);
        }

        //GET all items
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItemDtoAsync();
            return items == null ? NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }) : Ok(items);
        }

        //GET item by Id
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            var item = await _itemService.GetItemById(id);

            if (item == null)
            {
                return NotFound(new { ErrorCode = 404, Message = $"Item with ID {id} not found" });
            }

            return Ok(item);
        }

        //GET item by name
        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetItemByName([FromRoute] string name)
        {
            var item = await _itemService.GetItemByName(name);

            if (item == null)
            {
                return NotFound(new { ErrorCode = 404, Message = $"Item with ID {name} not found" });
            }

            return Ok(item);
        }

        //POST item
        [HttpPost("AddItem")]
        public async Task <IActionResult> AddItem(NewItemDTO dto)
        {
            if(dto != null) await _itemService.AddItemAsync(dto);
            
            return Ok(new { Message = "Item added successfully" });
        }

        //DELETE delete item by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult>  DeleteItem([FromRoute]int id) 
        {
            var isDeleted = await _itemService.DeleteItemAsync(id);
            
            return isDeleted == false ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                : (Ok(new { Message = "Item successfully deleted" }));
        }

        //PUT edit item by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpPut("EditItem/{id}")]
        public async Task<IActionResult> EditItemName([FromBody]EditItemDTO dto, [FromRoute] int id)
        {
            var isEdited = await _itemService.EditItemAsync(dto, id);

            return isEdited == false ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                : (Ok(new { Message = "Item successfully edited" }));
        }
    }
}
