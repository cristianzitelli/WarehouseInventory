namespace WarehouseInventory.Domain.Entities;

public class StockMovementReadEntity
{
    public Guid Id { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public DateTime OccurredAt { get; set; }
    public int Quantity { get; set; }
    public int NewQuantity { get; set; }
    public string Type { get; set; }
}

public class StockAddedReadEntity : StockMovementReadEntity
{
}

public class StockRemovedReadEntity : StockMovementReadEntity
{
}
