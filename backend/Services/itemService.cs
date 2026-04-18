using final_proj.DTO;
using final_proj.Models;

namespace final_proj.Services;

public class ItemService
{
    private readonly Db26TeamoneContext _context;

    public ItemService(Db26TeamoneContext context)
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