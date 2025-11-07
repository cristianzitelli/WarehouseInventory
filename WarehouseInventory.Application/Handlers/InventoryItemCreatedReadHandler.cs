using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Handlers
{
    public class InventoryItemCreatedReadHandler : IEventHandler<InventoryItemCreated>
    {
        private readonly IInventoryReadRepository _inventoryReadRepository;

        public InventoryItemCreatedReadHandler(IInventoryReadRepository inventoryReadRepository)
        {
            _inventoryReadRepository = inventoryReadRepository;
        }

        public async Task HandleAsync(InventoryItemCreated evt, CancellationToken cancellationToken = default)
        {
            var exists = await _inventoryReadRepository.GetBySkuAsync(evt.Sku);
            if (exists != null)
                return;

            await _inventoryReadRepository.AddAsync(evt);
        }

    }
}
