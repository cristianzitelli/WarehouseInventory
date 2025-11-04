namespace WarehouseInventory.Application.Handlers
{
    public interface IQueryHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
