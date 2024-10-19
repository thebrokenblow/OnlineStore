using MediatR;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class ProductRangeVM
{
    public required List<ProductVM> Products { get; set; }
}
