using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Entities;
using WarehouseInventory.Domain.Events;

namespace WarehouseInventory.Domain.Repositories
{
    public interface IInventoryReadRepository
    {
        Task<InventoryItem?> GetBySkuAsync(string sku);
        Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync();
        Task<IEnumerable<InventoryItem>> GetWithLowStockAsync(int lowStock);
        Task AddAsync(InventoryItemCreated evt);
        Task UpdateAsync(InventoryItem inventoryItem);
    }
}
