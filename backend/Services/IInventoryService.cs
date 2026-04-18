using System.Data;
using final_proj.DTO;
using final_proj.Models;
using final_proj.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace final_proj.Services;

public interface IInventoryService
{
    Task<InventoryResponseDto> StoreInventoryAsync(StoreInventoryRequestDto dto);
}

public class InventoryService : IInventoryService
{
    private readonly Db26TeamoneContext _context;

    public InventoryService(Db26TeamoneContext context)
    {
        _context = context;
    }

    public async Task<InventoryResponseDto> StoreInventoryAsync(StoreInventoryRequestDto dto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var itemExists = await _context.Items1.AnyAsync(i => i.Id == dto.ItemId);
        if (!itemExists)
        {
            throw new NotFoundApiException("Item not found.");
        }

        var binExists = await _context.Bins1.AnyAsync(b => b.Id == dto.BinId);
        if (!binExists)
        {
            throw new NotFoundApiException("Bin not found.");
        }

        var binConflict = await _context.InventoryRecords
            .AnyAsync(i => i.BinId == dto.BinId && i.ItemId != dto.ItemId);

        if (binConflict)
        {
            throw new ConflictApiException("A bin stores only one product.");
        }

        var inventory = await _context.InventoryRecords
            .FirstOrDefaultAsync(i => i.BinId == dto.BinId && i.ItemId == dto.ItemId);

        if (inventory == null)
        {
            throw new NotFoundApiException("Inventory must exist before it can be stored.");
        }

        var rowsUpdated = await _context.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE warehouse.app_inventory SET quantity = quantity + {dto.Quantity}, updated_at = now() WHERE id = {inventory.Id}"
        );

        if (rowsUpdated == 0)
        {
            throw new ConflictApiException("Inventory update failed due to concurrent modification.");
        }

        await transaction.CommitAsync();

        var refreshed = await _context.InventoryRecords.FirstAsync(i => i.Id == inventory.Id);
        return new InventoryResponseDto
        {
            Id = refreshed.Id,
            ItemId = refreshed.ItemId,
            BinId = refreshed.BinId,
            Quantity = refreshed.Quantity
        };
    }
}
