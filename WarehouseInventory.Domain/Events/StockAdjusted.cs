namespace WarehouseInventory.Domain.Events;

public record StockAdjusted(Guid Id, string Sku, int Quantity, string MovementType, DateTime OccurredAt, int NewQuantity);
