namespace WarehouseInventory.Domain.Entities;

public class InventoryItemReadEntity
{
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
}
