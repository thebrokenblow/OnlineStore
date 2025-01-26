using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineShop.Domain;

namespace OnlineShop.WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[ControllerName("productCategories")]
[Route("api/[controller]/[action]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductCategoryController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products
    /// </remarks>
    /// <returns>Returns list of getAllProductDto</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ActionName(nameof(GetAll))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GetAllProductCategoryDto>>> GetAll()
    {
        var allProductCategoryQuery = new GetAllProductCategoryQuery();
        var allProductCategories = await mediator.Send(allProductCategoryQuery);

        return Ok(allProductCategories);
    }

    /// <summary>
    /// Gets the product categories from the range
    /// </summary>
    /// <remarks>
    /// <param name="countSkip">count of product categories to skip (int)</param>
    /// <param name="countTake">count of product categories to take (int)</param>
    /// Sample request:
    /// GET /api/productCategories/0/10
    /// </remarks>
    /// <returns>Returns list of getAllProductCategoryDto</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If countSkip is less than zero or countTake is less than or equal to zero</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ActionName(nameof(GetRange))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<GetRangeProductCategoryDto>>> GetRange(int countSkip, int countTake)
    {
        var rangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = countSkip,
            CountTake = countTake
        };

        var rangeProductCategories = await mediator.Send(rangeProductCategoryQuery);

        return Ok(rangeProductCategories);
    }

    /// <summary>
    /// Gets the details product category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/productCategories/1
    /// </remarks>
    /// <param name="id">Product category id (int)</param>
    /// <returns>Returns ProductCategory</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If product category is not found by id</response>
    [HttpGet("{id}")]
    [ActionName(nameof(GetDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductCategory>> GetDetails(int id)
    {
        var detailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = id
        };

        var detailsProductCategory = await mediator.Send(detailsProductCategoryQuery);

        return Ok(detailsProductCategory);
    }

    /// <summary>
    /// Creates the product category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST api/productCategories
    /// {
    ///     name: "name product category",
    ///     description: "description product category"
    /// }
    /// </remarks>
    /// <param name="createProductCategoryCommand">CreateProductCategoryCommand object</param>
    /// <returns>Returns id (int)</returns>
    /// <response code="201">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters.
    /// </response>
    [HttpPost]
    [ActionName(nameof(Create))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCategoryCommand createProductCategoryCommand)
    {
        var productCategoryId = await mediator.Send(createProductCategoryCommand);

        return CreatedAtAction(nameof(Create), productCategoryId);
    }

    /// <summary>
    /// Updates the product category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT api/productCategories
    /// {
    ///     id: "id of the product category to update",
    ///     name: "updated product category name",
    ///     description: "updated product category description",
    /// }
    /// </remarks>
    /// <param name="updateProductCategoryCommand">UpdateProductCategoryCommand object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="400">
    /// If the name is empty or the length exceeds 250 characters, 
    /// the description is empty or the length exceeds 1024 characters.
    /// </response>
    /// <response code="404">If product category is not found by id</response>
    [HttpPut]
    [ActionName(nameof(Update))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> Update([FromBody] UpdateProductCategoryCommand updateProductCategoryCommand)
    {
        await mediator.Send(updateProductCategoryCommand);

        return NoContent();
    }

    /// <summary>
    /// Deletes the product category by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /productCategories/1
    /// </remarks>
    /// <param name="id">Id of the product category (int)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="404">If product category is not found</response>
    [HttpDelete("{id}")]
    [ActionName(nameof(Delete))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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