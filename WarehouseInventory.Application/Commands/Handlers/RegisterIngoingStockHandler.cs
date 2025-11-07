using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Commands.Handlers;

public class RegisterIngoingStockHandler : ICommandHandler<RegisterIngoingStockCommand>
{
    private readonly IInventoryRepository _inventoryRepo;
    private readonly IOutboxRepository _outboxRepo;

    public RegisterIngoingStockHandler(IInventoryRepository repo, IOutboxRepository outboxRepo)
    {
        _inventoryRepo = repo;
        _outboxRepo = outboxRepo;
    }

    public async Task HandleAsync(RegisterIngoingStockCommand command)
    {
        var item = await _inventoryRepo.GetBySkuAsync(command.Sku)
            ?? throw new InvalidOperationException("Item not found");

        item.IncreaseStock(command.Quantity);

        await _inventoryRepo.UpdateAsync(item);

        await _outboxRepo.AddAsync(new StockAdded(Guid.NewGuid(), command.Sku, command.Quantity, DateTime.UtcNow, item.Quantity));
    }
}

