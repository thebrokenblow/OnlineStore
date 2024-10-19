using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;

public class ProductCategoryRangeVM
{
    public required List<ProductCategory> ProductCategories { get; set; }
}