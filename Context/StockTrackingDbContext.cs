using FiStockBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FiStockBackend.Context;
public class StockTrackingDbContext : DbContext
{
    public StockTrackingDbContext(DbContextOptions<StockTrackingDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<StockMovement> StockMovements { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Lot> Lots { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>()
            .HasKey(s => s.SupplierId);
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);
        modelBuilder.Entity<StockMovement>()
            .HasKey(sm => sm.StockMovementId);
        modelBuilder.Entity<Warehouse>()
            .HasKey(w => w.WarehouseId);
        modelBuilder.Entity<Lot>()
            .HasKey(l => l.LotId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<StockMovement>()
            .HasOne(sm => sm.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(sm => sm.ProductId);

        modelBuilder.Entity<StockMovement>()
            .HasOne(sm => sm.SourceDestination)
            .WithMany(w => w.StockMovements)
            .HasForeignKey(sm => sm.SourceDestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany()
            .HasForeignKey(p => p.SupplierId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Lots)
            .WithOne(l => l.Product)
            .HasForeignKey(l => l.ProductId);

        modelBuilder.Entity<Supplier>()
            .Property(s => s.SupplierCode)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.SupplierId)
            .IsRequired();
    }
}