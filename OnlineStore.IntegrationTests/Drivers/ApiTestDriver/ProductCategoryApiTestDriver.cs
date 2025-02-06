using OnlineStore.IntegrationTests.Fixture;

namespace OnlineStore.IntegrationTests.Drivers.ApiTestDriver;

public class ProductCategoryApiTestDriver(ITestServerFixture fixture)
{
    private const string nameController = "productCategories";
    private readonly HttpClient _httpClient = fixture.HttpClient;
}