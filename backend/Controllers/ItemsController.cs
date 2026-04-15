using Microsoft.AspNetCore.Mvc;
using final_proj.DTO;
using final_proj.Services;

namespace final_proj.Controllers;

[ApiController]
[Route("api/item")]
public class ItemsController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemsController(ItemService itemService)
    {
        _itemService = itemService;
    }

    // POST: api/items
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemDto dto)
    {
        var createdItem = await _itemService.CreateItem(dto);
        return Ok(createdItem);
    }
}