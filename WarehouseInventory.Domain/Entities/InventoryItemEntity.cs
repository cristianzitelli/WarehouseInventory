namespace WarehouseInventory.Domain.Entities;

public class InventoryItemEntity
{
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
}
