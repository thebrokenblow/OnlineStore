using MediatR;
using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class ProductRangeVM
{
    public required List<Product> Products { get; set; }
}
