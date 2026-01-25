using System;
using System.Runtime.CompilerServices;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        //Table Name
        builder.ToTable("StockMovements");

        //Primary Key
        builder.HasKey(sm => sm.Id);

        //Properties
        builder.Property(sm => sm.MovementType)
            .IsRequired()
            .HasConversion<string>(); //store enum as string in database

        builder.Property(sm => sm.Quantity)
            .IsRequired();

        builder.Property(sm => sm.MovementDate)
            .IsRequired();

        builder.Property(sm => sm.Reference)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(sm => sm.Notes)
            .IsRequired(false)
            .HasMaxLength(500);

        //Index
        builder.HasIndex(sm => sm.Reference); //for searching by reference
        builder.HasIndex(sm=>sm.MovementDate); //for date range queries

        //Relationships
        //with Product
        builder.HasOne(sm => sm.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(sm => sm.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        //with Warehouse
        builder.HasOne(sm=>sm.Warehouse)
            .WithMany(w=>w.StockMovements)
            .HasForeignKey(sm=>sm.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
