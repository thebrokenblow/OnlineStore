using OnlineShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Persistence.EntityTypeConfigurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder
            .ToTable("product_categories");

        builder
            .HasKey(productCategory => productCategory.Id);

        builder
            .HasIndex(productCategory => productCategory.Id)
            .IsUnique();

        builder
            .Property(productCategory => productCategory.Id)
            .HasColumnName("id");

        builder
            .Property(productCategory => productCategory.Name)
            .HasColumnName("name")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(productCategory => productCategory.Description)
            .HasColumnName("description")
            .HasMaxLength(1024)
            .IsRequired(false);
    }
}