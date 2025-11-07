using System.Text.Json;
using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Events;

namespace WarehouseInventory.Application.Dispatchers
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandler<InventoryItemCreated> _inventoryItemCreatedEventHandler;
        private readonly IEventHandler<StockAdded> _stockAddedEventHandler;
        private readonly IEventHandler<StockRemoved> _stockRemovedEventHandler;

        public EventDispatcher(IEventHandler<InventoryItemCreated> inventoryItemCreatedEventHandler, IEventHandler<StockAdded> stockAddedEventHandler, IEventHandler<StockRemoved> stockRemovedEventHandler)
        {
            _inventoryItemCreatedEventHandler = inventoryItemCreatedEventHandler;
            _stockAddedEventHandler = stockAddedEventHandler;
            _stockRemovedEventHandler = stockRemovedEventHandler;
        }

        public async Task DispatchAsync(OutboxItem evtMessage, CancellationToken cancellationToken = default)
        {
            switch (evtMessage.Type)
            {
                case nameof(InventoryItemCreated):
                    await _inventoryItemCreatedEventHandler.HandleAsync(evtMessage.GetDeserializedContent<InventoryItemCreated>(), cancellationToken);
                    break;
                case nameof(StockAdded):
                    await _stockAddedEventHandler.HandleAsync(evtMessage.GetDeserializedContent<StockAdded>(), cancellationToken);
                    break;
                case nameof(StockRemoved):
                    await _stockRemovedEventHandler.HandleAsync(evtMessage.GetDeserializedContent<StockRemoved>(), cancellationToken);
                    break;
                default:
                    throw new InvalidCastException("The event could not be deserialized");
            }
        }
    }
}
