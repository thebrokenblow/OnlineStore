using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.DeleteProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using OnlineShop.Application.Products.Queries.GetProductDetails;
using OnlineShop.Application.Products.Queries.GetProductRange;
using OnlineShop.Domain;
using OnlineShop.Application.Products.Queries.GetProductId;

namespace OnlineShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet("{countSkip}/{countTake}")]
    [ActionName(nameof(GetProductRange))]
    public async Task<ActionResult<ProductRangeVM>> GetProductRange(int countSkip, int countTake)
    {
        var query = new GetProductRangeQuery
        {
            CountSkip = countSkip,
            CountTake = countTake
        };

        var vm = await mediator.Send(query);

        return Ok(vm);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetById))]
    public async Task<ActionResult<GetProductIdVM>> GetById(int id)
    {
        var query = new GetProductIdQuery
        {
            Id = id
        };

        var vm = await mediator.Send(query);
        return Ok(vm);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetails))]
    public async Task<ActionResult<Product>> GetDetails(int id)
    {
        var query = new GetProductDetailsQuery
        {
            Id = id
        };

        var vm = await mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    [ActionName(nameof(Create))]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand createProductCommand)
    {
        var productId = await mediator.Send(createProductCommand);

        return Ok(productId);
    }

    [HttpPut]
    [ActionName(nameof(Update))]
    public async Task<ActionResult<int>> Update([FromBody] UpdateProductCommand updateProductCommand)
    {
        var productId = await mediator.Send(updateProductCommand);

        return Ok(productId);
    }

    [HttpDelete("{id}")]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProductCommand
        {
            Id = id,
        };
        await mediator.Send(command);

        return NoContent();
    }
}