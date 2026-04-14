using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; } // Add DbSet for Customer

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            // Configure Value Converter for Sku
            entity.Property(p => p.ProductSku)
                .HasConversion(
                    sku => sku.Value,          // Convert Sku to string for storage
                    value => new Sku(value));  // Convert string from database back to Sku

            // Configure Money as an Owned Entity Type
            entity.OwnsOne(p => p.Price, money =>
            {
                money.Property(m => m.Amount).HasColumnName("PriceAmount");
                money.Property(m => m.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasConversion(
                        c => c.ToString(),     // Convert enum to string for storage
                        s => (MoneyCurrency)Enum.Parse(typeof(MoneyCurrency), s)); // Convert string back to enum
            });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            // Configure Address as an Owned Entity Type
            entity.OwnsOne(c => c.ShippingAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("ShippingStreet");
                address.Property(a => a.City).HasColumnName("ShippingCity");
                address.Property(a => a.State).HasColumnName("ShippingState");
                address.Property(a => a.PostalCode).HasColumnName("ShippingPostalCode");
                address.Property(a => a.Country).HasColumnName("ShippingCountry");
            });
        });
    }
}
