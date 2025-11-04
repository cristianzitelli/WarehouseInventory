namespace WarehouseInventory.Domain.Entities;

public class OutboxMessageEntity
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!; // JSON
    public DateTime? ProcessedAt { get; set; }
}
