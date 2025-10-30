using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Commands.Handlers;

public class RegisterIngoingStockHandler
{
    private readonly IInventoryRepository _repo;

    public RegisterIngoingStockHandler(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(RegisterIngoingStockCommand command)
    {
        var item = await _repo.GetBySkuAsync(command.Sku)
            ?? throw new InvalidOperationException("Item not found");

        item.IncreaseStock(command.Quantity);
        await _repo.UpdateAsync(item);
    }
}

