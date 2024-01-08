using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("Item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController( IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllItems()
        {
           var items = await _itemService.GetAllItemDtoAsync();
            return items == null ? NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }) : Ok(items);
        }
        [HttpPost("AddItem")]
        public async Task <IActionResult> AddItem(ItemDTO dto)
        {
            if(dto != null) await _itemService.AddItemAsync(dto);
            
            return Ok(new { Message = "Item added successfully" });
        }
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult>  DeleteItem([FromRoute]int id) 
        {
            var isDeleted = await _itemService.DeleteItemAsync(id);
            
            return isDeleted == false ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                : (Ok(new { Message = "Item successfully deleted" }));
        }
        [HttpPut("EditItem/{id}")]
        public async Task<IActionResult> EditItemName([FromBody]ItemDTO dto, [FromRoute] int id)
        {
            var isEdited = await _itemService.EditItemAsync(dto, id);

            return isEdited == false ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                : (Ok(new { Message = "Item successfully edited" }));
        }
    }
}
