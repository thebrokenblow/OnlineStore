using OnlineShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace OnlineShop.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(product => product.Id);

        builder
            .HasIndex(product => product.Id)
            .IsUnique();

        builder
            .Property(product => product.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasMaxLength(1024)
            .IsRequired();

        builder
            .Property(product => product.Price)
            .HasPrecision(10, 2)
            .IsRequired();
    }
}