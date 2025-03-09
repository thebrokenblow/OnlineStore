using OnlineShop.WebApi.Model.ProductCategory;
using OnlineStore.IntegrationTests.Drivers.ApiTestDriver.V1;
using OnlineStore.IntegrationTests.Drivers.TestData;
using OnlineStore.IntegrationTests.Fixture;
using Reqnroll;
using System.Net;

namespace OnlineStore.IntegrationTests.Steps;

[Binding]
public class ProductCategoryStepDefinitions(TestServerFixture fixture, ScenarioContext scenarioContext)
{
    private readonly ProductCategoryApiTestDriver _driver = new(fixture);
    private readonly Dictionary<string, int> _nameToIdMap = [];

    private Exception? _lastException;

    [Given(@"we add product categories:")]
    [When(@"we add product categories:")]
    public async Task WhenWeAddProductCategories(Table table)
    {
        try
        {
            var productCategories = table.CreateSet<TestProductCategoryData>().ToList();

            foreach (var productCategory in productCategories)
            {
                int productCategoryId = await _driver.AddAsync(productCategory);
                _nameToIdMap[productCategory.Name] = productCategoryId;
            }
        }
        catch (Exception ex) when (IsNegativeScenario())
        {
            _lastException = ex;
        }
    }

    [Then(@"we get list of product categories:")]
    public async Task ThenWeGetListOfProductCategories(Table table)
    {
        var expected = table.CreateSet<TestAllProductCategoryData>().ToList();
        var actual = await _driver.GetAllAsync();

        Assert.Equal(expected.Count, actual.Count);
        Assert.Equivalent(expected, actual);
    }

    [When(@"we update product categories:")]
    public async Task WhenWeUpdateProductCategories(Table table)
    {
        try
        {
            var productCategories = table.CreateSet<TestProductCategoryData>().ToList();

            var productCategory = productCategories.First();
            _nameToIdMap.TryGetValue("Fitness Equipment", out int idProductCategory);

            var updateProductCategoryModel = new UpdateProductCategoryModel
            {
                Id = idProductCategory,
                Name = productCategory.Name,
                Description = productCategory.Description,
            };

            await _driver.UpdateAsync(updateProductCategoryModel);
        }
        catch (Exception ex) when (IsNegativeScenario())
        {
            _lastException = ex;
        }
    }

    [When("we delete product category with name: {string}")]
    public async Task WhenWeDeleteProductCategoryWithName(string electronics0)
    {
        try
        {
            if (!_nameToIdMap.TryGetValue(electronics0, out int idProductCategory))
            {
                throw new ArgumentException($"Unexpected product category name {electronics0}");
            }
            await _driver.DeleteAsync(idProductCategory);
        }
        catch (Exception ex) when (IsNegativeScenario())
        {
            _lastException = ex;
        }
    }


    [Then(@"we get a validation error")]
    public void ThenWeGetAValidationError()
    {
        Assert.IsType<ApiClientException>(_lastException);
        if (_lastException is ApiClientException e)
        {
            Assert.Equal(HttpStatusCode.BadRequest, e.HttpStatusCode);
        }
    }

    [AfterScenario]
    private void AfterScenario()
    {
        if (IsNegativeScenario())
        {
            Assert.NotNull(_lastException);
        }
        else if (_lastException != null)
        {
            throw _lastException;
        }
    }

    private bool IsNegativeScenario()
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("negative");
    }
}