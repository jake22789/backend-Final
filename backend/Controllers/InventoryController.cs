using final_proj.DTO;
using final_proj.Services;
using final_proj.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_proj.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPost("inventory/store")]
    public async Task<IActionResult> StoreInventory([FromBody] StoreInventoryRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _inventoryService.StoreInventoryAsync(dto);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    private IActionResult MapApiException(ApiException ex)
    {
        return ex switch
        {
            BadRequestApiException => BadRequest(new ApiErrorResponseDto { Error = ex.Message }),
            NotFoundApiException => NotFound(new ApiErrorResponseDto { Error = ex.Message }),
            ConflictApiException => Conflict(new ApiErrorResponseDto { Error = ex.Message }),
            _ => BadRequest(new ApiErrorResponseDto { Error = ex.Message })
        };
    }
}
