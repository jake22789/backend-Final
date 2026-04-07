using Microsoft.AspNetCore.Mvc;
using ClassLibrary.DTOs;
using final_proj.Services;

namespace final_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiveShipmentController : ControllerBase
    {
        private readonly IReceiveShipmentService _receiveShipmentService;

        public ReceiveShipmentController(IReceiveShipmentService receiveShipmentService)
        {
            _receiveShipmentService = receiveShipmentService;
        }

        [HttpPost]
        public async Task<ActionResult<ReceiveShipmentDTO>> ReceiveShipment([FromBody] ReceiveShipmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _receiveShipmentService.ReceiveShipmentAsync(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
