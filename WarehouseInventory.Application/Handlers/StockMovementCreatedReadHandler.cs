using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Handlers
{
    public class StockMovementCreatedReadHandler : IEventHandler<StockAdjusted>
    {
        private readonly IInventoryReadRepository _inventoryReadRepository;

        public StockMovementCreatedReadHandler(IInventoryReadRepository inventoryReadRepository)
        {
            _inventoryReadRepository = inventoryReadRepository;
        }

        public async Task HandleAsync(StockAdjusted evt, CancellationToken cancellationToken = default)
        {
            var exists = await _inventoryReadRepository.GetMovementByIdAsync(evt.Id);
            if (exists != null)
                return; 

            var item = await _inventoryReadRepository.GetBySkuAsync(evt.Sku);
            if (item == null)
                return;

            item.Quantity = evt.NewQuantity;

            await _inventoryReadRepository.UpdateAsync(item);

            await _inventoryReadRepository.AddAsync(evt);
        }

    }
}
