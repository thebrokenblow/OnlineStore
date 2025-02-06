using MediatR;

namespace OnlineShop.Application.Products.Queries.GetRangeProduct;

public class GetRangeProductQuery : IRequest<List<RangeProductDto>>
{
    public int CountSkip { get; set; }
    public int CountTake { get; set; }
}