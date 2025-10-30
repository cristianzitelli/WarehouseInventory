using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Domain.Entities;

namespace WarehouseInventory.Infrastructure;

public class InventoryDbContext : DbContext
{
    public DbSet<InventoryItemEntity> Items { get; set; }

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
    }
}
