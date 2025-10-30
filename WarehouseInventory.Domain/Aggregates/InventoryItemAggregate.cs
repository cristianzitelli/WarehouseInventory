namespace WarehouseInventory.Domain.Aggregates
{
    public class InventoryItem
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public InventoryItem(string sku, string name, int initialQty = 0)
        {
            Sku = sku;
            Name = name;
            Quantity = initialQty;
        }

        public void IncreaseStock(int qty)
        {
            if (qty <= 0) throw new ArgumentException("Quantity must be positive", nameof(qty));
            Quantity += qty;
        }

        public void DecreaseStock(int qty)
        {
            if (qty <= 0) throw new ArgumentException("Quantity must be positive", nameof(qty));
            if (Quantity - qty < 0) throw new InvalidOperationException("Insufficient stock");
            Quantity -= qty;
        }
    }
}
