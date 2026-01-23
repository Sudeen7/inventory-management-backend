using System;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        //Table Name
        builder.ToTable("Warehouses");

        //Primary Key
        builder.HasKey(w => w.Id);

        //Properties
        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Capacity)
            .IsRequired(false);

        //Index
        builder.HasIndex(w => w.Name)
            .IsUnique();

        //Relationships
        builder.HasMany(w => w.WarehouseProducts)
            .WithOne(wp => wp.Warehouse)
            .HasForeignKey(wp => wp.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
