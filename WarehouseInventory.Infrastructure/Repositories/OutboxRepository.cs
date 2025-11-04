using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Infrastructure.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly InventoryDbContext _context;

        public OutboxRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InventoryItemCreated evt)
        {
            var outbox = new OutboxMessageEntity
            {
                Id = Guid.NewGuid(),
                DateTime = evt.OccurredAt,
                Type = nameof(InventoryItemCreated),
                Content = JsonSerializer.Serialize(evt)
            };
            _context.OutboxMessages.Add(outbox);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(StockAdjusted evt)
        {
            var outbox = new OutboxMessageEntity
            {
                Id = Guid.NewGuid(),
                DateTime = evt.OccurredAt,
                Type = nameof(StockAdjusted),
                Content = JsonSerializer.Serialize(evt)
            };
            _context.OutboxMessages.Add(outbox);
            await _context.SaveChangesAsync();
        }

        public async Task<OutboxItem?> GetByIdAsync(Guid guid)
        {
            var outbox = await _context.OutboxMessages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == guid);
            return outbox == null ? null : new OutboxItem(outbox.Id, outbox.DateTime, outbox.Type, outbox.Content, outbox.ProcessedAt);
        }

        public async Task<IEnumerable<OutboxItem>> GetUnprocessedAsync(CancellationToken stoppingToken)
           => await _context.OutboxMessages
            .Where(o => o.ProcessedAt == null)
            .OrderBy(o => o.DateTime)
            .Take(50)
            .Select(o => new OutboxItem(o.Id, o.DateTime, o.Type, o.Content, null))
            .ToListAsync(stoppingToken);

        public async Task MarkProcessed(Guid guid)
        {
            var outbox = await _context.OutboxMessages.FindAsync(guid);
            if (outbox is null)
                return;

            outbox.ProcessedAt = DateTime.UtcNow;
            _context.OutboxMessages.Update(outbox);
            await _context.SaveChangesAsync();
        }
    }
}
