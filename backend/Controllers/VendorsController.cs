using Microsoft.AspNetCore.Mvc;
using final_proj.DTO;
using final_proj.Services;
using final_proj.Models;

namespace final_proj.Controllers;

[ApiController]
[Route("api/vendors")]
public class VendorController : ControllerBase
{
    private readonly VendorService _vendorService;

    public VendorController(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    // POST: api/vendor
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateVendorDto dto)
    {
        var createdVendor = await _vendorService.CreateVendor(dto);
        return Ok(createdVendor);
    }
}