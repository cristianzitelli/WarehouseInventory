using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Commands.Handlers;

public class AddInventoryItemHandler
{
    private readonly IInventoryRepository _repo;

    public AddInventoryItemHandler(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<string> HandleAsync(AddInventoryItemCommand command)
    {
        var item = await _repo.GetBySkuAsync(command.Sku);
        if(item != null) 
            throw new InvalidOperationException("Item already exists");

        var newitem = new InventoryItem(command.Sku, command.Name);
        await _repo.AddAsync(newitem);
        return newitem.Sku;
    }
}

