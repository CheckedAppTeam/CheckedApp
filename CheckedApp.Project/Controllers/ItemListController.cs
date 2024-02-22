using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [Route("ItemList")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemListController : ControllerBase
    {
        private readonly IItemListService _itemListService;
        private readonly ILogger<ItemListController> _logger;

        public ItemListController(
            IItemListService itemListService,
            ILogger<ItemListController> logger
        )
        {
            _itemListService = itemListService;
            _logger = logger;
        }

        //GET all lists
        [HttpGet("GetAllLists")]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetAllAsync()
        {
            var itemListsDto = await _itemListService.GetAllAsync();

            if (itemListsDto is null)
            {
                _logger.LogWarning("No Item List found");
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        //GET all itemLists of the user by user Id
        [HttpGet("User/{userid}")]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetAllByUserIdAsync([FromRoute] string userid)
        {
            var itemListsDto = await _itemListService.GetAllByUserIdAsync(userid);

            if (itemListsDto is null)
            {
                _logger.LogWarning("No Item List found");
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        //GET itemList data by itemList Id
        [HttpGet("Getlist/{itemlistid}")]
        public async Task<ActionResult<ItemListDTO>> GetList([FromRoute] int itemlistid)
        {
            var itemList = await _itemListService.GetByIdAsync(itemlistid);

            if (itemList is null)
            {
                _logger.LogWarning($"Item List with id {itemList} not found");
                return NotFound();
            }

            return Ok(itemList);
        }

        //GET all itemLists by destination name
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetByCityAsync([FromRoute] string city)
        {
            var itemLists = await _itemListService.GetByCityAsync(city);

            if (itemLists is null)
            {
                _logger.LogWarning("Item List not found");
                return NotFound();
            }

            return Ok(itemLists);
        }

        //GET all itemLists by destination and date
        [HttpGet("CityAndDate/{city}/{date}")]
        public async Task<ActionResult<ItemListDTO>> GetByDateAndCity([FromRoute] string city,[FromRoute] DateTime date)
        {
            var itemList = await _itemListService.GetByMonthAndCity(date, city);

            if (itemList is null)
            {
                return NotFound();
            }

            return Ok(itemList);
        }

        //POST new list to user by user Id
        [HttpPost("AddList/{userid}")]
        public async Task<ActionResult> AddList([FromBody] CreateItemListDTO dto,[FromRoute] string userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _itemListService.CreateAsync(dto, userid);

            _logger.LogInformation($"ItemList {dto.ItemListName} is added");
            return Ok(new { Message = "Item List added successfully" });
        }

        //GET and POST copy a existing list to own itemList list by itemList Id and to user by his Id
        [HttpPost("User/{itemListid}/{userid}")]
        public async Task<ActionResult> CopyItemListAsync([FromRoute] int itemListid,[FromRoute] string userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var copy = await _itemListService.CopyAsync(itemListid, userid);

            _logger.LogInformation($"Item List with id {itemListid} is copied");
            return Ok(copy);
        }

        //PUT edit itemList data by itemList Id
        [HttpPut("EditListSpecification/{id}")]
        public async Task<ActionResult> UpdateItemListAsync([FromBody] UpdateItemListDTO dto,[FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _itemListService.UpdateAsync(dto, id);

            if (isUpdated)
            {
                _logger.LogInformation($"Item List {dto.ItemListName} is created");
                return NoContent();
            }

            _logger.LogWarning($"Item List with id {id} is not found");
            return NotFound();
        }

        //DELETE delete itemlist by itemList Id
        [HttpDelete("DeleteList/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var isDeleted = await _itemListService.DeleteAsync(id);

            if (isDeleted)
            {
                _logger.LogInformation($"Item List with id {id} is deleted");
                return NoContent();
            }

            _logger.LogInformation($"Item List with id {id} is deleted");
            return NotFound();
        }
    }
}
