using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQuery : IRequest<List<GetRangeProductCategoryDto>>
{
    public required int CountSkip { get; set; }
    public required int CountTake { get; set; }
}