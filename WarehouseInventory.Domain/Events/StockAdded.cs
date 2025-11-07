namespace WarehouseInventory.Domain.Events;

public record StockAdded(Guid Id, string Sku, int Quantity, DateTime OccurredAt, int NewQuantity) : BaseEvent(OccurredAt);
