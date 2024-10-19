using MediatR;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class GetProductRangeQuery : IRequest<ProductRangeVM>
{
    public int CountSkip { get; set; }
    public int CountTake { get; set; }
}