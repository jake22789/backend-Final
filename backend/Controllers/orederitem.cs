using Microsoft.AspNetCore.Mvc;
using final_proj.DTO;
using final_proj.Services;
using final_proj.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace final_proj.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class OrdersController : ControllerBase
{
    private readonly IOrderWorkflowService _orderService;

    public OrdersController(IOrderWorkflowService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateCustomerOrderRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _orderService.CreateOrderAsync(dto);
            return Created($"/orders/{created.Id}", created);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("orders/{id:int}/pick")]
    public async Task<IActionResult> PickOrder(int id)
    {
        try
        {
            var result = await _orderService.PickOrderAsync(id);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("orders/{id:int}/pack")]
    public async Task<IActionResult> PackOrder(int id)
    {
        try
        {
            var result = await _orderService.PackOrderAsync(id);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("orders/{id:int}/ship")]
    public async Task<IActionResult> ShipOrder(int id)
    {
        try
        {
            var result = await _orderService.ShipOrderAsync(id);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderStatus(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new ApiErrorResponseDto { Error = "Order ID must be greater than 0" });
        }

        try
        {
            var result = await _orderService.GetOrderStatusAsync(id);
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