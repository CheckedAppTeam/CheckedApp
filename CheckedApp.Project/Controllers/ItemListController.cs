using CheckedAppProject.DATA;
using Microsoft.AspNetCore.Mvc;
using CheckedAppProject.LOGIC.Services.Logger;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemListDTO>>> GetAllAsync()
        {
            var itemListsDto = await _itemListService.GetAllAsync();

            if (itemListsDto is null)
            {
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemListDTO>> GetAsync([FromRoute] int id)
        {
            var itemList = await _itemListService.GetByIdAsync(id);

            if (itemList is null)
            {
                return NotFound();
            }

            return Ok(itemList);
        }

        [HttpPost]
        public async Task<ActionResult> CreateItemList([FromBody] CreateItemListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _itemListService.CreateAsync(dto);

            return Created($"api/itemList/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemList([FromRoute] int id, [FromBody] UpdateItemListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _itemListService.UpdateAsync(id, dto);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _itemListService.DeleteAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        //private readonly IAppLogger _logger ;
        //public ItemListController(IAppLogger logger)
        //{
        //    _logger = logger;
        //}

        //public void AddList(string name) 
        //{
        //    try
        //    {
        //        _logger.LogToFileAndConsole("Successfuly added a list.");


        //    }catch (Exception ex)
        //    {
        //        _logger.LogException(ex);
        //    }
        //}
        //public List<Item> GetList() { return null; }
        //public List<List<Item>> GetAllLists() { return null; }
        //public void UpdateList(List<Item> list) { }
        //public void DeleteList(string name) { }
        //public void PublishList(string name) { }
        //public void TakeDownList(string name) { }
        //public List<string> GetAllListNames() { return null; }
    }
}
