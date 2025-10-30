using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Commands.Handlers;

public class RegisterOutgoingStockHandler
{
    private readonly IInventoryRepository _repo;

    public RegisterOutgoingStockHandler(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(RegisterOutgoingStockCommand command)
    {
        var item = await _repo.GetBySkuAsync(command.Sku)
            ?? throw new InvalidOperationException("Item not found");

        item.DecreaseStock(command.Quantity);
        await _repo.UpdateAsync(item);
    }
}

