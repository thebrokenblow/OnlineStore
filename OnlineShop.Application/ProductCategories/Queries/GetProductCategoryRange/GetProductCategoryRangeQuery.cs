using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;

public class GetProductCategoryRangeQuery : IRequest<ProductCategoryRangeVM>
{
    public int CountSkip { get; set; }
    public int CountTake { get; set; }
}
