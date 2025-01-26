using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQuery : IRequest<List<GetRangeProductCategoryDto>>
{
    public int CountSkip { get; set; }
    public int CountTake { get; set; }
}
