using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Queries.Handlers;

public class GetInventoryItemHandler : IQueryHandler<GetInventoryItemQuery, InventoryItem?>
{
    private readonly IInventoryRepository _repo;

    public GetInventoryItemHandler(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<InventoryItem?> HandleAsync(GetInventoryItemQuery query) => await _repo.GetBySkuAsync(query.Sku);

}

