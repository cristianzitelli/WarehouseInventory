using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Infrastructure.Repositories
{
    public class InventoryReadRepository : IInventoryReadRepository
    {
        private readonly InventoryReadDbContext _context;

        public InventoryReadRepository(InventoryReadDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync() 
            => await _context.Items
                .Select(i => new InventoryItem(i.Sku, i.Name, i.Quantity))
                .ToListAsync();
        
        public async Task<InventoryItem?> GetBySkuAsync(string sku)
        {
            var item = await _context.Items.FindAsync(sku);
            return item == null ? null : new InventoryItem(item.Sku, item.Name, item.Quantity);
        }

        public async Task<IEnumerable<InventoryItem>> GetWithLowStockAsync(int lowStock) 
            => await _context.Items.Where(i => i.Quantity <= lowStock)
                .Select(i => new InventoryItem(i.Sku, i.Name, i.Quantity))
                .ToListAsync();

        public async Task AddAsync(InventoryItemCreated evt)
        {
            var item = new InventoryItemReadEntity
            {
                Sku = evt.Sku,
                Name = evt.Name,
                Quantity = evt.InitialQty
            };
            _context.Items.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InventoryItem inventoryItem)
        {
            var item = await _context.Items.FindAsync(inventoryItem.Sku)
                ?? throw new InvalidOperationException("Product not found");
            item.Quantity = inventoryItem.Quantity;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
