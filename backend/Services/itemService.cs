using final_proj.Data;
using final_proj.DTO;
using final_proj.Models;

namespace final_proj.Services;

public class ItemService
{
    private readonly AppDbContext _context;

    public ItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Item> CreateItem(CreateItemDto dto)
    {
        var item = new Item
        {
            Name = dto.Name,
            Price = dto.Price,
            Volume = dto.Volume
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }
}