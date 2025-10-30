using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Queries.Handlers;

public class GetAllInventoryItemsHandler
{
    private readonly IInventoryRepository _repo;

    public GetAllInventoryItemsHandler(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<InventoryItem>> HandleAsync(GetAllInventoryItemsQuery query) => await _repo.GetAllInventoryItemsAsync();

}

