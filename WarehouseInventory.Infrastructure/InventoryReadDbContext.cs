using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Entities;

namespace WarehouseInventory.Infrastructure;

public class InventoryReadDbContext : DbContext
{
    public DbSet<InventoryItemReadEntity> Items { get; set; }
    public DbSet<StockMovementReadEntity> Movements { get; set; }

    public InventoryReadDbContext(DbContextOptions<InventoryReadDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItemReadEntity>(b =>
        {
            b.ToTable("inventory_item_read");

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

        modelBuilder.Entity<StockMovementReadEntity>(b =>
        {
            b.ToTable("stock_movement_read");

            b.HasKey(p => p.Id);

            b.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            b.Property(p => p.Sku)
                .HasColumnName("sku")
                .IsRequired()
                .HasMaxLength(50);

            b.Property(p => p.Type)
                .HasColumnName("type")
                .IsRequired()
                .HasMaxLength(100);

            b.Property(p => p.OccurredAt)
                .HasColumnName("occurred_at")
                .IsRequired()
                .HasMaxLength(2000);

            b.Property(p => p.Quantity)
                .HasColumnName("quantity");

            b.Property(p => p.NewQuantity)
                .HasColumnName("new_quantity");

            b.HasDiscriminator<string>(p => p.Type)
            .HasValue<StockAddedReadEntity>(Domain.Enums.StockMovement.Ingoing.ToString())
            .HasValue<StockRemovedReadEntity>(Domain.Enums.StockMovement.Outgoing.ToString());
        });

        modelBuilder.Entity<StockAddedReadEntity>().HasBaseType<StockMovementReadEntity>();
        modelBuilder.Entity<StockRemovedReadEntity>().HasBaseType<StockMovementReadEntity>();

    }
}
