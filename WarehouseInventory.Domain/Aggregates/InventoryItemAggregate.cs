using System.Text.Json;
using WarehouseInventory.Domain.Events;

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

    public class OutboxItem
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; } = default!;
        public string Content { get; set; } = default!; // JSON
        public DateTime? ProcessedAt { get; set; }

        public T GetDeserializedContent<T>() where T: BaseEvent => JsonSerializer.Deserialize<T>(Content) ;

        public OutboxItem(Guid id, DateTime dateTime, string type, string content, DateTime? processedAt)
        {
            Id = id;
            DateTime = dateTime;
            Type = type;
            Content = content;
            ProcessedAt = processedAt;
        }
    }

    public class StockMovement
    {
        public string Sku { get; set; } = default!;
        public DateTime OccurredAt { get; set; }
        public int Quantity { get; set; }
        public int NewQuantity { get; set; }
        public string Type { get; set; }

        public StockMovement(string sku, DateTime occurredAt, string type, int quantity, int newQuantity)
        {
            Sku = sku;
            OccurredAt = occurredAt;
            Type = type;
            Quantity = quantity;
            NewQuantity = newQuantity;
        }

        public override string ToString() => $"{Sku}: {(Type.Equals(Domain.Enums.StockMovement.Ingoing.ToString()) ? "+" : "-")}{Quantity} @ {OccurredAt.ToString("G")}";
    }
}
