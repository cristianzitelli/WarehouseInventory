using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InventoryItem inventoryItem)
        {
            var item = new InventoryItemEntity
            {
                Sku = inventoryItem.Sku,
                Name = inventoryItem.Name,
                Quantity = 0
            };
            _context.Items.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task<InventoryItem?> GetBySkuAsync(string sku)
        {
            var item = await _context.Items.AsNoTracking().FirstOrDefaultAsync(p => p.Sku == sku);
            return item == null ? null : new InventoryItem(item.Sku, item.Name, item.Quantity);
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
