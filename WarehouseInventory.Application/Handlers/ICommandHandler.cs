namespace WarehouseInventory.Application.Handlers
{
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
