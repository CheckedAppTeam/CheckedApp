using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckedAppProject.API.Controllers
{
    public class ItemController : ControllerBase
    {
        private readonly IAppLogger _logger;
        private readonly UserItemContext _dbContext;
        public ItemController(IAppLogger logger, UserItemContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllItems()
        {
            var items = _dbContext.Items.ToList();
            return Ok(items);
        }
        [HttpPost]
        public IActionResult AddItem(int id, string name, string? company)
        {
            var newItem = new Item
            {
                ItemId = id,
                ItemName = name,
                ItemCompany = company
            };
            _dbContext.Items.Add(newItem);
            _dbContext.SaveChanges();

            _logger.LogToConsole($"Added item: {newItem.ItemName}");
            return Ok(newItem);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id) 
        {
            var itemToRemove = _dbContext.Items.Find(id);
            if (itemToRemove != null)
            {
                _dbContext.Items.Remove(itemToRemove);
                _dbContext.SaveChanges();

                _logger.LogToConsole($"Deleted item: {itemToRemove.ItemName}");
                return Ok();
            }
            else
            {
                _logger.LogException(null,$"Item with ID {id} not found");
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public IActionResult EditItemName(int id, [FromBody] string name)
        {
            var itemToEdit = _dbContext.Items.Find(id);
            if (itemToEdit != null)
            {
                itemToEdit.ItemName = name;
                _dbContext.SaveChanges();

                _logger.LogToConsole($"Edited item name: {itemToEdit.ItemName} to {name}");
                return Ok(itemToEdit);
            }
            else
            {
                _logger.LogException(null, $"Item with ID {id} not found");
                return NotFound();
            }
        }
        [HttpPut("EditItemCompanyName/{id}")]
        public IActionResult EditItemCompanyName(int id, [FromBody] string newCompanyName)
        {
            var itemToEdit = _dbContext.Items.Find(id);
            if (itemToEdit != null)
            {
                itemToEdit.ItemCompany = newCompanyName;
                _dbContext.SaveChanges();

                _logger.LogToConsole($"Edited item company name for item ID {id} to {newCompanyName}");
                return Ok(itemToEdit);
            }
            else
            {
                _logger.LogException(null, $"Item with ID {id} not found");
                return NotFound();
            }
        }
    }
}
