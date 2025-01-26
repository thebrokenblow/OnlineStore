using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain;

namespace OnlineShop.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("products");

        builder
            .HasKey(product => product.Id);

        builder
            .HasIndex(product => product.Id)
            .IsUnique();

        builder
            .Property(product => product.Id)
            .HasColumnName("id");

        builder
            .Property(product => product.Name)
            .HasColumnName("name")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasColumnName("description")
            .HasMaxLength(1024)
            .IsRequired();

        builder
            .Property(product => product.Price)
            .HasColumnName("price")
            .HasPrecision(10, 2)
            .IsRequired();

        builder
            .Property(product => product.ProductCategoryId)
            .HasColumnName("product_category_id");

        builder
            .HasOne(product => product.ProductCategory)
            .WithMany()
            .HasForeignKey(product => product.ProductCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}