using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers;

[ApiController]
[Route("Map")]

public class MapController : ControllerBase
{
    private readonly IItemListService _itemListService;
    private readonly ILogger<MapController> _logger;

    public MapController(IItemListService itemListService, ILogger<MapController> logger)
    {
        _itemListService = itemListService;
        _logger = logger;
    }

    //GET all destinations from API
    [HttpGet("GetAllDestinations")]
    public async Task<ActionResult<IEnumerable<ListDestinationDTO>>> GetAllDestinationsAsync()
    {
        var publicItemLists = await _itemListService.GetPublicListsAsync();

        if (publicItemLists is null || !publicItemLists.Any())
        {
            _logger.LogWarning("No public Item Lists found");
            return NotFound();
        }

        var itemListsDto = publicItemLists.Select(itemList => new ListDestinationDTO
        {
            ItemListId = itemList.ItemListId,
            ItemListName = itemList.ItemListName,
            ItemListDestination = itemList.ItemListDestination
        }).ToList();

        return Ok(itemListsDto);
    }
}
