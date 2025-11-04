namespace WarehouseInventory.Domain.Entities;

public class StockMovementReadEntity
{
    public Guid Id { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public DateTime OccurredAt { get; set; }
    public int Quantity { get; set; }
    public int NewQuantity { get; set; }
    public required string Type { get; set; }
}
