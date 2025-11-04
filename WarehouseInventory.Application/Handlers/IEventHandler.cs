namespace WarehouseInventory.Application.Commands.Handlers
{
    public interface IEventHandler<TEvent>
    {
        Task HandleAsync(TEvent evt, CancellationToken cancellationToken = default);
    }
}
