using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Commands.Handlers;

public class AddInventoryItemHandler : ICommandHandler<AddInventoryItemCommand>
{
    private readonly IInventoryRepository _inventoryRepo;
    private readonly IOutboxRepository _outboxRepo;

    public AddInventoryItemHandler(IInventoryRepository repo, IOutboxRepository outboxRepo)
    {
        _inventoryRepo = repo;
        _outboxRepo = outboxRepo;
    }

    public async Task HandleAsync(AddInventoryItemCommand command)
    {
        var item = await _inventoryRepo.GetBySkuAsync(command.Sku);
        if (item != null)
            throw new InvalidOperationException("Item already exists");

        var newitem = new InventoryItem(command.Sku, command.Name);

        await _inventoryRepo.AddAsync(newitem);

        await _outboxRepo.AddAsync(new InventoryItemCreated(command.Sku, command.Name, newitem.Quantity, DateTime.UtcNow));
    }
}

