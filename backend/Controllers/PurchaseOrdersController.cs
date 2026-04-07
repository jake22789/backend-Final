using Microsoft.AspNetCore.Mvc;
using final_proj.Models;
using final_proj.Services;
using ClassLibrary.DTOs;

namespace final_proj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseOrdersController : ControllerBase
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly ILogger<PurchaseOrdersController> _logger;

    public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService, ILogger<PurchaseOrdersController> logger)
    {
        _purchaseOrderService = purchaseOrderService;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new purchase order
    /// </summary>
    /// <param name="dto">Create Purchase Order Request DTO</param>
    /// <returns>The created purchase order</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder([FromBody] CreatePurchaseOrderDTO dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for CreatePurchaseOrder");
            return BadRequest(ModelState);
        }

        try
        {
            var createdOrder = await _purchaseOrderService.CreatePurchaseOrderAsync(dto);
            return CreatedAtAction(nameof(CreatePurchaseOrder), new { id = createdOrder.Id }, createdOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating purchase order: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the purchase order" });
        }
    }
}
