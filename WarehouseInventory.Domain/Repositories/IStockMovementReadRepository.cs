using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Events;

namespace WarehouseInventory.Domain.Repositories
{
    public interface IStockMovementReadRepository
    {
        Task<StockMovementReadEntity?> GetMovementByIdAsync(Guid id);
        Task<IEnumerable<StockMovement>> GetAllMovementsAsync();
        Task AddAsync(StockAdded evt);
        Task AddAsync(StockRemoved evt);
    }
}
