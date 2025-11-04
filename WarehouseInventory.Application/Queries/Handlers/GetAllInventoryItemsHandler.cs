using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Queries.Handlers;

public class GetAllInventoryItemsHandler : IQueryHandler<GetAllInventoryItemsQuery, IEnumerable<InventoryItem>>
{
    private readonly IInventoryReadRepository _repo;

    public GetAllInventoryItemsHandler(IInventoryReadRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<InventoryItem>> HandleAsync(GetAllInventoryItemsQuery query) => await _repo.GetAllInventoryItemsAsync();

}
