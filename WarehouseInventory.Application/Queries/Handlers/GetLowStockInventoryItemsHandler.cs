using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Queries.Handlers;

public class GetLowStockInventoryItemsHandler : IQueryHandler<GetLowStockInventoryItemsQuery, IEnumerable<InventoryItem>>
{
    private readonly IInventoryReadRepository _repo;

    public GetLowStockInventoryItemsHandler(IInventoryReadRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<InventoryItem>> HandleAsync(GetLowStockInventoryItemsQuery query) => await _repo.GetWithLowStockAsync(10);

}

