using final_proj.Data;
using final_proj.DTO;
using final_proj.Models;

namespace final_proj.Services;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateCustomer(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return customer;
    }
}