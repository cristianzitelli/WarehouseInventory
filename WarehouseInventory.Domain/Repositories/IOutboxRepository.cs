using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Events;

namespace WarehouseInventory.Domain.Repositories
{
    public interface IOutboxRepository
    {
        Task<OutboxItem?> GetByIdAsync(Guid guid);
        Task<IEnumerable<OutboxItem>> GetUnprocessedAsync(CancellationToken stoppingToken);
        Task AddAsync(InventoryItemCreated evt);
        Task AddAsync(StockAdded evt);
        Task AddAsync(StockRemoved evt);
        Task MarkProcessed(Guid guid);
    }
}
