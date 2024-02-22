using CheckedAppProject.DATA.Models;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("Item")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }
        
        //GET filtered items, one item - depending on the function provided - delegate
        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAllItemsPages([FromQuery] ItemsQuery query)
        {
            try
            {
                var items = await _itemService.GetAllItemDtoAsyncPages(query);
                return items == null
                    ? NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" })
                    : Ok(items);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all items pages");
                return StatusCode(500, "Internal server error");
            }
        }
        

        //GET all items
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItemDtoAsync();
                return items == null
                    ? NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" })
                    : Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all items");
                return StatusCode(500, "Internal server error");
            }
        }

        //GET item by Id
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            try
            {
                var item = await _itemService.GetItemById(id);

                if (item == null)
                {
                    return NotFound(new { ErrorCode = 404, Message = $"Item with ID {id} not found" });
                }

                return Ok(item);
            }catch(Exception ex)
            {
                _logger.LogError($"Error occured while getting item by id {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            }

        //GET item by name
        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetItemByName([FromRoute] string name)
        {
            try
            {
            var item = await _itemService.GetItemByName(name);

            if (item == null)
            {
                return NotFound(
                    new { ErrorCode = 404, Message = $"Item with ID {name} not found" }
                );
            }

            return Ok(item);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured while getting item by name {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //POST item
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(NewItemDTO dto)
        {
            try
            {
            if (dto != null)
                await _itemService.AddItemAsync(dto);


            return Ok(new { Message = "Item added successfully" });
            }catch(Exception ex)
            {
                _logger.LogError($"Error occured while adding item  {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //DELETE delete item by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            try
            {
                var isDeleted = await _itemService.DeleteItemAsync(id);

                return isDeleted == false
                    ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                    : (Ok(new { Message = "Item successfully deleted" }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting item with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        //PUT edit item by Id, only by ADMIN
        [Authorize(Roles = "Admin")]
        [HttpPut("EditItem/{id}")]
        public async Task<IActionResult> EditItemName([FromBody] EditItemDTO dto,[FromRoute] int id)
        {
            try
            {
            var isEdited = await _itemService.EditItemAsync(dto, id);

                return isEdited == false
                    ? (NotFound(new { ErrorCode = 404, Message = "Item with this ID not found" }))
                    : (Ok(new { Message = "Item successfully edited" }));
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while editing item");
                return StatusCode(500, "Internal server error");
            }
            
        }
    }
}
