using System;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        //Table Name
        builder.ToTable("Suppliers");

        //Primary Key
        builder.HasKey(s=>s.Id);

        //Properties
        builder.Property(s=>s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s=>s.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s=>s.Email)
            .HasMaxLength(25);

        builder.Property(s=>s.Phone)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(s=>s.CreatedAt)
            .IsRequired();

        builder.Property(s=>s.UpdatedAt)
            .IsRequired(false);

        //Index
        builder.HasIndex(s=>s.Name)
            .IsUnique();

        //Realtionships
        builder.HasMany(s=>s.Products)
            .WithOne(p=>p.Supplier)
            .HasForeignKey(p=>p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
