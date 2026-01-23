using System;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //Table Name
        builder.ToTable("Products");

        //Primary key
        builder.HasKey(p=>p.Id);

        //Properties
        builder.Property(p=>p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p=>p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p=>p.Description)
            .HasMaxLength(1000);

        builder.Property(p=>p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p=>p.MinimumStockLevel)
            .IsRequired();

        builder.Property(p=>p.CreatedAt)
            .IsRequired();

        builder.Property(pp=>pp.UpdatedAt)
            .IsRequired(false);

        //Index
        builder.HasIndex(p=>p.SKU)
            .IsUnique();

        //Relationships
        builder.HasOne(p=>p.Category)
            .WithMany(c=>c.Products)
            .HasForeignKey(p=>p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p=>p.Supplier)
            .WithMany(s=>s.Products)
            .HasForeignKey(p=>p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p=>p.WarehouseProducts)
            .WithOne(wp=>wp.Product)
            .HasForeignKey(wp=>wp.ProductId)
            .OnDelete(DeleteBehavior.Cascade); //delete warehouseProducts when Product is deleted

        builder.HasMany(p=>p.StockMovements)
            .WithOne(sm=>sm.Product)
            .HasForeignKey(sm=>sm.ProductId)
            .OnDelete(DeleteBehavior.Restrict); //keep movement records even if products are deleted
            
    }
}
