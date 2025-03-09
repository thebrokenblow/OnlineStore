using OnlineShop.WebApi.Model.Product;
using OnlineStore.IntegrationTests.Drivers.ApiTestDriver.V1;
using OnlineStore.IntegrationTests.Drivers.TestData;
using OnlineStore.IntegrationTests.Fixture;
using Reqnroll;

namespace OnlineStore.IntegrationTests.Steps;

[Binding]
public class ProductStepDefinitions(TestServerFixture fixture, ScenarioContext scenarioContext)
{
    private readonly ProductApiTestDriver productApi = new(fixture);
    private readonly ProductCategoryApiTestDriver productCategoryApi = new(fixture);

    private readonly Dictionary<string, int> _idProductCategoryByName = [];


    [Given("we want to add several products, but to add products we have to add categories:")]
    public async Task GivenWeWantToAddProductsButToAddProductsWeHaveToAddCategories(DataTable productCategorisTable)
    {
        var productCategories = productCategorisTable.CreateSet<TestProductCategoryData>().ToList();

        foreach (var productCategory in productCategories)
        {
            var id = await productCategoryApi.AddAsync(productCategory);
            _idProductCategoryByName[productCategory.Name] = id;
        }
    }

    [When("we add several products:")]
    public async Task WhenWeAddProduct(DataTable productsTable)
    {
        var products = productsTable.CreateSet<TestProductData>().ToList();

        foreach (var product in products)
        {
            var createProductModel = new CreateProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdProductCategory = _idProductCategoryByName[product.CategoryName]
            };

            var id = await productApi.AddAsync(createProductModel);
            _idProductCategoryByName[product.Name] = id;
        }
    }

    [Then("we can get all records of products:")]
    public async Task ThenWeCanGetAllRecordsOfProducts(DataTable productsTable)
    {
        var expected = productsTable.CreateSet<TestAllProductData>().ToList();
        var actual = await productApi.GetAllAsync();

        Assert.Equal(expected.Count, actual.Count);
        Assert.Equivalent(expected, actual);
    }
}