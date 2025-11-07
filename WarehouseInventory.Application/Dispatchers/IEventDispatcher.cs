using WarehouseInventory.Domain.Aggregates;

namespace WarehouseInventory.Application.Dispatchers
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(OutboxItem evt, CancellationToken cancellationToken = default);
    }
}
