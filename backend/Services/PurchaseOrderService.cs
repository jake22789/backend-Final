using System;
using System.Collections.Generic;
using final_proj.Models;
using ClassLibrary.DTOs;
using Microsoft.EntityFrameworkCore;

namespace final_proj.Services
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrderDTO> CreatePurchaseOrderAsync(PurchaseOrderDTO dto);
    }
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly Db26TeamoneContext _context;

        public PurchaseOrderService(Db26TeamoneContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrderDTO> CreatePurchaseOrderAsync(PurchaseOrderDTO dto)
        {
           
            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new ArgumentException("Purchase order must contain at least one item.");

            foreach (var item in dto.OrderItems)
            {
                if (item.Quantity == null || item.Quantity <= 0)
                    throw new ArgumentException("Each order item must have Quantity > 0.");
            }

            
            var purchaseOrder = new PurchaseOrder
            {
                DateOrdered = dto.DateOrdered ?? DateTime.UtcNow,
                OrderItems = dto.OrderItems.Select(oi => new OrderItem
                {
                    ItemId = oi.ItemId ?? 0,
                    Quantity = oi.Quantity ?? 0,
                    UnitPrice = oi.UnitPrice ?? 0
                }).ToList()
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            
            dto.Id = purchaseOrder.Id;
            dto.OrderItems = purchaseOrder.OrderItems.Select(oi => new OrderItemDTO
            {
                Id = oi.Id,
                ItemId = oi.ItemId,
                OrderId = oi.OrderId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList();

            return dto;
        }
    }
}