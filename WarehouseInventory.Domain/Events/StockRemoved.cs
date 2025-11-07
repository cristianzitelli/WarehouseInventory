namespace WarehouseInventory.Domain.Events;

public record StockRemoved(Guid Id, string Sku, int Quantity, DateTime OccurredAt, int NewQuantity) : BaseEvent(OccurredAt);
