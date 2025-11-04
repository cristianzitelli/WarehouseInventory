using System.Text.Json;
using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Events;

namespace WarehouseInventory.Application.Dispatchers
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandler<InventoryItemCreated> _inventoryItemCreatedEventHandler;
        private readonly IEventHandler<StockAdjusted> _stockAdjustedEventHandler;

        public EventDispatcher(IEventHandler<InventoryItemCreated> inventoryItemCreatedEventHandler, IEventHandler<StockAdjusted> stockAdjustedEventHandler)
        {
            _inventoryItemCreatedEventHandler = inventoryItemCreatedEventHandler;
            _stockAdjustedEventHandler = stockAdjustedEventHandler;
        }

        public async Task DispatchAsync(OutboxItem evtMessage, CancellationToken cancellationToken = default)
        {
            switch (evtMessage.Type)
            {
                case nameof(InventoryItemCreated):
                    await _inventoryItemCreatedEventHandler.HandleAsync(JsonSerializer.Deserialize<InventoryItemCreated>(evtMessage.Content), cancellationToken);
                    break;
                case nameof(StockAdjusted):
                    await _stockAdjustedEventHandler.HandleAsync(JsonSerializer.Deserialize<StockAdjusted>(evtMessage.Content), cancellationToken);
                    break;
                default:
                    throw new InvalidCastException("The event could not be deserialized");
            }
        }
    }
}
