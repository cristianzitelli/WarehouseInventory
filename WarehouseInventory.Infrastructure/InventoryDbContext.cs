using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Entities;

namespace WarehouseInventory.Infrastructure;

public class InventoryDbContext : DbContext
{
    public DbSet<InventoryItemEntity> Items { get; set; }
    public DbSet<OutboxMessageEntity> OutboxMessages { get; set; }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItemEntity>(b =>
        {
            b.ToTable("inventory_item");

            b.HasKey(p => p.Sku);

            b.Property(p => p.Sku)
                .HasColumnName("sku")
                .IsRequired()
                .HasMaxLength(50);

            b.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            b.Property(p => p.Quantity)
                .HasColumnName("quantity")
                .IsRequired();
        });

        modelBuilder.Entity<OutboxMessageEntity>(b =>
        {
            b.ToTable("outbox_message");

            b.HasKey(p => p.Id);

            b.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            b.Property(p => p.DateTime)
                .HasColumnName("datetime")
                .IsRequired();

            b.Property(p => p.Type)
                .HasColumnName("type")
                .IsRequired()
                .HasMaxLength(200);

            b.Property(p => p.Content)
                .HasColumnName("content")
                .IsRequired()
                .HasMaxLength(2000);

            b.Property(p => p.ProcessedAt)
                .HasColumnName("processed_at");
        });
    }
}
