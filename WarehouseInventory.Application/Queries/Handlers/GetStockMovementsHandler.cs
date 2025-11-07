using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Queries.Handlers;

public class GetStockMovementsHandler : IQueryHandler<GetStockMovementsQuery, IEnumerable<StockMovement>>
{
    private readonly IStockMovementReadRepository _repo;

    public GetStockMovementsHandler(IStockMovementReadRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<StockMovement>> HandleAsync(GetStockMovementsQuery query) 
        => await _repo.GetAllMovementsAsync();

}

