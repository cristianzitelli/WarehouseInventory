using WarehouseInventory.Domain.Aggregates;

namespace WarehouseInventory.Domain.Repositories
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetBySkuAsync(string sku);
        Task AddAsync(InventoryItem product);
        Task UpdateAsync(InventoryItem product);
    }
}
