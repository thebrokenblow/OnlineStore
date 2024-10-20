﻿using OnlineShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Persistence.EntityTypeConfigurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder
            .HasKey(productCategory => productCategory.Id);

        builder
            .HasIndex(productCategory => productCategory.Id)
            .IsUnique();

        builder
            .Property(productCategory => productCategory.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(productCategory => productCategory.Description)
            .HasMaxLength(1024)
            .IsRequired();
    }
}