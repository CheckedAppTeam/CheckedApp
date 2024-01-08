using CheckedAppProject.DATA;
using Microsoft.AspNetCore.Mvc;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.LOGIC.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using CheckedAppProject.LOGIC.Services;

namespace CheckedAppProject.API.Controllers
{
    [Route("api/itemlist")]
    public class ItemListController : ControllerBase
    {
        private readonly IItemListService _itemListService;

        public ItemListController(IItemListService itemListService)
        {
            _itemListService = itemListService;
        }

        [HttpGet("getalllists")]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetAllAsync()
        {
            var itemListsDto = await _itemListService.GetAllAsync();

            if (itemListsDto is null)
            {
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetAllByUserIdAsync([FromRoute] int userid)
        {
            var itemListsDto = await _itemListService.GetAllByUserIdAsync(userid);

            if (itemListsDto is null)
            {
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        [HttpGet("getlist/{itemlistid}")]
        public async Task<ActionResult<ItemListDTO>> GetList([FromRoute] int itemlistid)
        {
            var itemList = await _itemListService.GetByIdAsync(itemlistid);

            if (itemList is null)
            {
                return NotFound();
            }

            return Ok(itemList);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<ItemListDTO>> GetByCityAsync([FromRoute] string city)
        {
            var itemList = await _itemListService.GetByCityAsync(city);

            if (itemList is null)
            {
                return NotFound();
            }

            return Ok(itemList);
        }

        //[HttpGet("cityanddate/{city}/{date}")]
        //public async Task<AcceptedResult<ItemListDTO>> GetByDateAndCity([FromRoute] string city, [FromRoute] DateTime date)
        //{
        //    var itemList = await _itemListService.GetByCityAndDateAsync(city, date);

        //    if (itemList is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(itemList);
        //}

        [HttpPost("addlist/{userid}")]
        public async Task<ActionResult> AddList([FromBody] CreateItemListDTO dto, [FromRoute] int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _itemListService.CreateAsync(dto, userid);

            return Ok(new { Message = "Item List added successfully" });
        }

        [HttpPost("user/{userid}")]
        public async Task<ActionResult> CopyItemListAsync([FromRoute] int itemListid, [FromRoute] int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var copy = await _itemListService.CopyAsync(itemListid, userid);

            return Ok(copy);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemListAsync([FromBody] UpdateItemListDTO dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _itemListService.UpdateAsync(dto, id);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var isDeleted = await _itemListService.DeleteAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
