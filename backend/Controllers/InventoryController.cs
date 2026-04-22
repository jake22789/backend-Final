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

    [HttpGet("inventory")]
    public async Task<IActionResult> GetInventory([FromQuery] int? productId)
    {
        try
        {
            if (productId.HasValue)
            {
                if (productId.Value <= 0)
                {
                    return BadRequest(new ApiErrorResponseDto { Error = "Product ID must be greater than 0" });
                }

                var detailedResult = await _inventoryService.GetInventoryByProductAsync(productId.Value);
                return Ok(detailedResult);
            }
            else
            {
                var reportResult = await _inventoryService.GetInventoryReportAsync();
                return Ok(reportResult);
            }
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

    [HttpGet("inventory/on-hand")]
    public async Task<IActionResult> GetInventoryOnHand()
    {
        var result = await _inventoryService.GetInventoryOnHandAsync();
        return Ok(result);
    }

    [HttpGet("inventory/items-needing-to-be-shipped")]
    public async Task<IActionResult> GetItemsNeedingToBeShipped()
    {
        var result = await _inventoryService.GetItemsNeedingToBeShippedAsync();
        return Ok(result);
    }

    [HttpGet("orders/needing-to-be-shipped")]
    public async Task<IActionResult> GetOrdersNeedingToBeShipped()
    {
        var result = await _inventoryService.GetOrdersNeedingToBeShippedAsync();
        return Ok(result);
    }

    [HttpGet("reports/profitability")]
    public async Task<IActionResult> GetProfitabilityPerItem()
    {
        var result = await _inventoryService.GetProfitabilityPerItemAsync();
        return Ok(result);
    }
}
