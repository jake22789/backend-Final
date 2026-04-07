using Microsoft.AspNetCore.Mvc;
using ClassLibrary.DTOs;
using final_proj.Services;

namespace final_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDTO>> CreatePurchaseOrder([FromBody] PurchaseOrderDTO dto)
        {
            try
            {
                var result = await _purchaseOrderService.CreatePurchaseOrderAsync(dto);
                return CreatedAtAction(nameof(CreatePurchaseOrder), result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
