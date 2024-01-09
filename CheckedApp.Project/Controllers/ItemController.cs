using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly UserItemContext _dbContext;
        private readonly IItemService _itemService;
        public ItemController(ILogger<ItemController> logger, UserItemContext dbContext, IItemService itemService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _itemService = itemService;
        }
        [HttpGet("/ItemController/GetAll")]
        public IActionResult GetAllItems()
        {
            var items = _dbContext.Items.ToList();

            return Ok(items);
        }
        [HttpPost("/ItemController/AddItem")]
        public async Task <IActionResult> AddItem(ItemDTO dto)
        {
           
            _logger.LogInformation($"Added item:");
            return Ok(new { Message = "Item created successfully" });

        }
        [HttpDelete("/ItemController/DeleteItem/{id}")]
        public IActionResult DeleteItem(int id) 
        {
            var itemToRemove = _dbContext.Items.Find(id);
            if (itemToRemove != null)
            {
                _dbContext.Items.Remove(itemToRemove);
                _dbContext.SaveChanges();
                
                _logger.LogInformation($"Deleted item: {itemToRemove.ItemName}");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Item with ID {id} not found");
                return NotFound();
            }
        }
        [HttpPut("/ItemController/EditItemName/{id}")]
        public IActionResult EditItemName(int id, [FromBody] string name)
        {
            var itemToEdit = _dbContext.Items.Find(id);
            if (itemToEdit != null)
            {
                itemToEdit.ItemName = name;
                _dbContext.SaveChanges();

                _logger.LogInformation($"Edited item name: {itemToEdit.ItemName} to {name}");
                return Ok(itemToEdit);
            }
            else
            {
                _logger.LogInformation( $"Item with ID {id} not found");
                return NotFound();
            }
        }
        [HttpPut("/ItemController/EditItemCompanyName/{id}")]
        public IActionResult EditItemCompanyName(int id, [FromBody] string newCompanyName)
        {
            var itemToEdit = _dbContext.Items.Find(id);
            if (itemToEdit != null)
            {
                itemToEdit.ItemCompany = newCompanyName;
                _dbContext.SaveChanges();

                _logger.LogInformation($"Edited item company name for item ID {id} to {newCompanyName}");
                return Ok(itemToEdit);
            }
            else
            {
                _logger.LogInformation( $"Item with ID {id} not found");
                return NotFound();
            }
        }
    }
}
