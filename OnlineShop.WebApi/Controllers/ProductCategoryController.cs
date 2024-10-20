using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;
using OnlineShop.Application.ProductCategories.Commands.DeleteProductCategory;
using OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetProductCategoryId;
using OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;
using OnlineShop.Domain;

namespace OnlineShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductCategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet("{countSkip}/{countTake}")]
    [ActionName(nameof(GetRange))]
    public async Task<ActionResult<ProductCategoryRangeVM>> GetRange(int countSkip, int countTake)
    {
        var query = new GetProductCategoryRangeQuery
        {
            CountSkip = countSkip,
            CountTake = countTake
        };

        var vm = await mediator.Send(query);

        return Ok(vm);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(Get))]
    public async Task<ActionResult<ProductCategory>> Get(int id)
    {
        var query = new GetProductCategoryIdQuery
        {
            Id = id
        };
        var vm = await mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    [ActionName(nameof(Create))]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCategoryCommand createProductCategoryCommand)
    {
        var productCategoryId = await mediator.Send(createProductCategoryCommand);

        return Ok(productCategoryId);
    }

    [HttpPut]
    [ActionName(nameof(Update))]
    public async Task<ActionResult<int>> Update([FromBody] UpdateProductCategoryCommand updateProductCategoryCommand)
    {
        var productCategoryId = await mediator.Send(updateProductCategoryCommand);

        return Ok(productCategoryId);
    }

    [HttpDelete("{id}")]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProductCategoryCommand
        {
            Id = id,
        };
        await mediator.Send(command);

        return NoContent();
    }
}