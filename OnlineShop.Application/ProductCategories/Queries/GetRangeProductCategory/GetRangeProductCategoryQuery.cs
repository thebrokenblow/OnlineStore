using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQuery : IRequest<List<RangeProductCategoryDto>>
{
    public required int CountSkip { get; set; }
    public required int CountTake { get; set; }
}