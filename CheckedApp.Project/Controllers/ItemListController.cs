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

        [HttpGet]
        public ActionResult<IEnumerable<ItemListDTO>> GetAll()
        {
            var itemListsDto = _itemListService.GetAll();

            if (itemListsDto is null)
            {
                return NotFound();
            }

            return Ok(itemListsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ItemListDTO> Get([FromRoute] int id)
        {
            var itemList = _itemListService.GetById(id);

            if (itemList is null)
            {
                return NotFound();
            }

            return Ok(itemList);
        }

        [HttpPost]
        public ActionResult CreateItemList([FromBody] CreateItemListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _itemListService.Create(dto);

            return Created($"api/itemList/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItemList([FromRoute] int id, [FromBody] UpdateItemListDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _itemListService.Update(id, dto);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _itemListService.Delete(id);

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
