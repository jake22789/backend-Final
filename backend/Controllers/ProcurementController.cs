using final_proj.DTO;
using final_proj.Services;
using final_proj.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_proj.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class ProcurementController : ControllerBase
{
    private readonly IProcurementService _procurementService;

    public ProcurementController(IProcurementService procurementService)
    {
        _procurementService = procurementService;
    }

    [HttpPost("purchase-orders")]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _procurementService.CreatePurchaseOrderAsync(dto);
            return Created($"/purchase-orders/{created.Id}", created);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("shipments")]
    public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _procurementService.CreateShipmentAsync(dto);
            return Created($"/shipments/{created.Id}", created);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("shipments/{id:int}/receive")]
    public async Task<IActionResult> ReceiveShipment(int id)
    {
        try
        {
            var result = await _procurementService.ReceiveShipmentAsync(id);
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
