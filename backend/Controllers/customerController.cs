using Microsoft.AspNetCore.Mvc;
using final_proj.DTO;
using final_proj.Services;
using final_proj.Models;

namespace final_proj.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    // POST: api/customer
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        var createdCustomer = await _customerService.CreateCustomer(dto);
        return Ok(createdCustomer);
    }
}