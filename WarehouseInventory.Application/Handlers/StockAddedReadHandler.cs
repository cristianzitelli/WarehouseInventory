using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Application.Handlers
{
    public class StockAddedReadHandler : IEventHandler<StockAdded>
    {
        private readonly IInventoryReadRepository _inventoryReadRepository;
        private readonly IStockMovementReadRepository _stockMovementReadRepository;

        public StockAddedReadHandler(IInventoryReadRepository inventoryReadRepository, IStockMovementReadRepository stockMovementReadRepository)
        {
            _inventoryReadRepository = inventoryReadRepository;
            _stockMovementReadRepository = stockMovementReadRepository;
        }

        public async Task HandleAsync(StockAdded evt, CancellationToken cancellationToken = default)
        {
            try
            {
                var exists = await _stockMovementReadRepository.GetMovementByIdAsync(evt.Id);
                if (exists != null)
                    return; 

                var item = await _inventoryReadRepository.GetBySkuAsync(evt.Sku);
                if (item == null)
                    return;

                item.Quantity = evt.NewQuantity;

                await _inventoryReadRepository.UpdateAsync(item);

                await _stockMovementReadRepository.AddAsync(evt);
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
