using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Infrastructure.Repositories
{
    public class StockMovementReadRepository : IStockMovementReadRepository
    {
        private readonly InventoryReadDbContext _context;

        public StockMovementReadRepository(InventoryReadDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StockAdded evt)
        {
            var movement = new StockAddedReadEntity
            {
                Id = Guid.NewGuid(),
                Sku = evt.Sku,
                Quantity = evt.Quantity,
                NewQuantity = evt.NewQuantity,
                OccurredAt = evt.OccurredAt
            };
            _context.Movements.Add(movement);

            await _context.SaveChangesAsync();
        }

        public async Task<StockMovementReadEntity?> GetMovementByIdAsync(Guid id) 
            => await _context.Movements.FindAsync(id);

        public async Task<IEnumerable<StockMovement>> GetAllMovementsAsync()
            => await _context.Movements
                .Select(i => new StockMovement(i.Sku, i.OccurredAt, i.Type, i.Quantity, i.NewQuantity))
                .ToListAsync();

        public async Task AddAsync(StockRemoved evt)
        {
            var movement = new StockRemovedReadEntity
            {
                Id = Guid.NewGuid(),
                Sku = evt.Sku,
                Quantity = evt.Quantity,
                NewQuantity = evt.NewQuantity,
                OccurredAt = evt.OccurredAt
            };
            _context.Movements.Add(movement);

            await _context.SaveChangesAsync();
        }
    }
}
