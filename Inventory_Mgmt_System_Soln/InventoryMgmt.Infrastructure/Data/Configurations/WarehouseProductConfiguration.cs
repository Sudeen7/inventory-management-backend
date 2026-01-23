using System;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class WarehouseProductConfiguration : IEntityTypeConfiguration<WarehouseProduct>
{
    public void Configure(EntityTypeBuilder<WarehouseProduct> builder)
    {
        //Table Name
        builder.ToTable("WarehouseProducts");

        //Primary Key
        builder.HasKey(wp => wp.Id);

        //Properties
        builder.Property(wp => wp.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(wp => wp.CreatedAt)
            .IsRequired();

        builder.Property(wp => wp.UpdatedAt)
            .IsRequired(false);

        //Index (unique constraint on combination of ProductId+WarehouseId)
        builder.HasIndex(wp=>new{wp.ProductId, wp.WarehouseId})
            .IsUnique();

        //Relationships
        //with Product
        builder.HasOne(wp => wp.Product)
            .WithMany(p => p.WarehouseProducts)
            .HasForeignKey(wp => wp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        //with Warehouse
        builder.HasOne(wp => wp.Warehouse)
            .WithMany(w => w.WarehouseProducts)
            .HasForeignKey(wp => wp.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
