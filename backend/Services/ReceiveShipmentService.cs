using Microsoft.EntityFrameworkCore;
using ClassLibrary.DTOs;
using final_proj.Models;

namespace final_proj.Services
{
    public interface IReceiveShipmentService
    {
        Task<ReceiveShipmentDTO> ReceiveShipmentAsync(ReceiveShipmentDTO dto);
    }

    public class ReceiveShipmentService : IReceiveShipmentService
    {
        private readonly Db26TeamoneContext _context;

        public ReceiveShipmentService(Db26TeamoneContext context)
        {
            _context = context;
        }

        public async Task<ReceiveShipmentDTO> ReceiveShipmentAsync(ReceiveShipmentDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id == null || dto.Id <= 0)
                throw new ArgumentException("A valid Shipment Id is required.", nameof(dto.Id));

            if (dto.OrderId == null || dto.OrderId <= 0)
                throw new ArgumentException("A valid OrderId is required.", nameof(dto.OrderId));

            if (dto.ItemShipments == null || !dto.ItemShipments.Any())
                throw new ArgumentException("Shipment must include at least one item.", nameof(dto.ItemShipments));

            var shipment = await _context.Shipments
                .Include(s => s.ItemShipments)
                .FirstOrDefaultAsync(s => s.Id == dto.Id.Value);

            if (shipment == null)
                throw new KeyNotFoundException($"Shipment with Id {dto.Id} does not exist.");

            if (shipment.OrderId != dto.OrderId)
                throw new ArgumentException("OrderId does not match the existing shipment.", nameof(dto.OrderId));

            var itemIds = dto.ItemShipments.Select(i => i.ItemId!.Value).Distinct().ToList();
            var existingItemIds = await _context.Items
                .Where(i => itemIds.Contains(i.Id))
                .Select(i => i.Id)
                .ToListAsync();

            var missingItemIds = itemIds.Except(existingItemIds).ToList();
            if (missingItemIds.Any())
                throw new ArgumentException($"Invalid ItemId(s): {string.Join(", ", missingItemIds)}.");

            foreach (var itemDto in dto.ItemShipments)
            {
                if (itemDto.Quantity == null || itemDto.Quantity <= 0)
                    throw new ArgumentException("Each item shipment must have a positive quantity.", nameof(itemDto.Quantity));

                if (itemDto.ItemId == null || itemDto.ItemId <= 0)
                    throw new ArgumentException("Each item shipment must have a valid ItemId.", nameof(itemDto.ItemId));

                var itemShipment = new ItemShipment
                {
                    ItemId = itemDto.ItemId.Value,
                    Quantity = itemDto.Quantity.Value,
                    Discount = itemDto.Discount,
                    Shipment = shipment
                };

                shipment.ItemShipments.Add(itemShipment);

                var binLocation = await _context.BinLocations
                    .Where(bl => bl.ItemId == itemDto.ItemId.Value)
                    .OrderBy(bl => bl.Id)
                    .FirstOrDefaultAsync();

                if (binLocation != null)
                {
                    binLocation.Quantity = (binLocation.Quantity ?? 0) + itemDto.Quantity.Value;
                }
                else
                {
                    _context.BinLocations.Add(new BinLocation
                    {
                        ItemId = itemDto.ItemId.Value,
                        Quantity = itemDto.Quantity.Value
                    });
                }
            }

            shipment.DateReceived = dto.DateReceived ?? DateTime.UtcNow;
            shipment.PriceAdjust = dto.PriceAdjust;
            shipment.HandlingCost = dto.HandlingCost;

            await _context.SaveChangesAsync();

            dto.ItemShipments = shipment.ItemShipments.Select(i => new itemShipmentDTO
            {
                Id = i.Id,
                ShipmentId = i.ShipmentId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                Discount = i.Discount
            }).ToList();

            return dto;
        }
    }
}
