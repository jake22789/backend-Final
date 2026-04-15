using final_proj.Data;
using final_proj.DTO;
using final_proj.Models;

namespace final_proj.Services;

public class VendorService
{
    private readonly AppDbContext _context;

    public VendorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vendor> CreateVendor(CreateVendorDto dto)
    {
        var vendor = new Vendor
        {
            Name = dto.Name
        };

        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();

        return vendor;
    }
}