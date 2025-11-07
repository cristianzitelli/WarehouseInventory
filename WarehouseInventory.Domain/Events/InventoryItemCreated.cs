namespace WarehouseInventory.Domain.Events;

public record InventoryItemCreated(string Sku, string Name, int InitialQty, DateTime OccurredAt) : BaseEvent(OccurredAt);
