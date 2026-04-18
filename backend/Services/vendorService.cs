using final_proj.DTO;
using final_proj.Models;

namespace final_proj.Services;

public class VendorService
{
    private readonly Db26TeamoneContext _context;

    public VendorService(Db26TeamoneContext context)
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