using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Products.Commands.ProductCreation;
using OnlineShop.Application.Products.Commands.ProductDeletion;
using OnlineShop.Application.Products.Commands.ProductUpdate;
using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineShop.WebApi.Model.Product;

namespace OnlineShop.WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[ControllerName("products")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets the all products
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products
    /// </remarks>
    /// <returns>Returns list of allProductDto</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ActionName(nameof(GetAll))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AllProductDto>>> GetAll()
    {
        var allProductQuery = new GetAllProductQuery();
        var allProductDto = await mediator.Send(allProductQuery);

        return Ok(allProductDto);
    }

    /// <summary>
    /// Gets the products from the range
    /// </summary>
    /// <remarks>
    /// <param name="countSkip">count of products to skip (int)</param>
    /// <param name="countTake">count of products to take (int)</param>
    /// Sample request:
    /// GET /api/products/0/10
    /// </remarks>
    /// <returns>Returns list of rangeProductDto</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If countSkip is less than zero or countTake is less than or equal to zero</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ActionName(nameof(GetRange))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<RangeProductDto>>> GetRange(int countSkip, int countTake)
    {
        var rangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake
        };

        var rangeProductDto = await mediator.Send(rangeProductQuery);

        return Ok(rangeProductDto);
    }

    /// <summary>
    /// Gets the details product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products/1
    /// </remarks>
    /// <param name="id">Product id (int)</param>
    /// <returns>Returns detailsProductDto</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If product category is not found by id</response>
    [HttpGet("{id}")]
    [ActionName(nameof(GetDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsProductDto>> GetDetails(int id)
    {
        var detailsProductQuery = new GetDetailsProductQuery
        {
            Id = id
        };

        var detailsProductDto = await mediator.Send(detailsProductQuery);

        return Ok(detailsProductDto);
    }

    /// <summary>
    /// Creates the product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST api/products
    /// {
    ///     name: "name product",
    ///     description: "description product",
    ///     price: "price product",
    ///     idProductCategory: "id of product category"
    /// }
    /// </remarks>
    /// <param name="createProductModel">CreateProductModel object</param>
    /// <returns>Returns id (int)</returns>
    /// <response code="201">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters,
    /// the price is less or equal 0.
    /// </response>
    [HttpPost]
    [ActionName(nameof(Create))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductModel createProductModel)
    {
        var createProductCommand = new CreateProductCommand
        {
            Name = createProductModel.Name,
            Description = createProductModel.Description,
            Price = createProductModel.Price,
            IdProductCategory = createProductModel.IdProductCategory,
        };

        var productId = await mediator.Send(createProductCommand);

        return CreatedAtAction(nameof(Create), productId);
    }

    /// <summary>
    /// Updates the product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT api/products
    /// {
    ///     id: "id of the product to update",
    ///     name: "name product",
    ///     description: "description product",
    ///     price: "price product",
    ///     idProductCategory: "id of product category"
    /// }
    /// </remarks>
    /// <param name="updateProductModel">UpdateProductModel object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters,
    /// the price is less or equal 0.
    /// </response>
    /// <response code="404">If product is not found by id</response>
    [HttpPut]
    [ActionName(nameof(Update))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromBody] UpdateProductModel updateProductModel)
    {
        var updateProductCommand = new UpdateProductCommand
        {
            Id = updateProductModel.Id,
            Name = updateProductModel.Name,
            Description = updateProductModel.Description,
            Price = updateProductModel.Price,
            IdProductCategory = updateProductModel.IdProductCategory,
        };

        await mediator.Send(updateProductCommand);

        return NoContent();
    }

    /// <summary>
    /// Deletes the product by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /products/1
    /// </remarks>
    /// <param name="id">Id of the product (int)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="404">If product is not found</response>
    [HttpDelete("{id}")]
    [ActionName(nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteProductCommand = new DeleteProductCommand
        {
            Id = id,
        };

        await mediator.Send(deleteProductCommand);

        return NoContent();
    }
}