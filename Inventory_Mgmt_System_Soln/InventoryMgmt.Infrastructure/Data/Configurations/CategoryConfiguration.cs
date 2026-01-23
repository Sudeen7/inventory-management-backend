using System;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryMgmt.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        //Table Name
        builder.ToTable("Categories");

        //primary key
        builder.HasKey(c=>c.Id);

        //properties
        builder.Property(c=>c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c=>c.Description)
            .HasMaxLength(500);

        builder.Property(c=>c.CreatedAt)
            .IsRequired();

        builder.Property(c=>c.UpdatedAt)
            .IsRequired(false);

        //Indexes
        builder.HasIndex(c=>c.Name)
            .IsUnique();

        //Relationships
        builder.HasMany(c=>c.Products) //category has MANY products
            .WithOne(p=>p.Category)    //product has ONE category
            .HasForeignKey(p=>p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); //don't allow deleting category with products
    }
}
