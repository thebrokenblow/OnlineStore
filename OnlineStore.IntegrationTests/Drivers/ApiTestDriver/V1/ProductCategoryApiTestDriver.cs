using Newtonsoft.Json;
using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.WebApi.Model.ProductCategory;
using OnlineStore.IntegrationTests.Drivers.TestData;
using OnlineStore.IntegrationTests.Fixture;
using System.Net.Http.Json;

namespace OnlineStore.IntegrationTests.Drivers.ApiTestDriver.V1;

public class ProductCategoryApiTestDriver(ITestServerFixture fixture)
{
    private const string nameController = "productCategories";
    private readonly HttpClient _httpClient = fixture.HttpClient;
    private const string version = "v1";

    public async Task<List<AllProductCategoryDto>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync($"/api/{version}/{nameController}");
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<AllProductCategoryDto>>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<int> AddAsync(TestProductCategoryData productCategory)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/{version}/{nameController}", productCategory);
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        var id = int.Parse(content);

        return id;
    }

    public async Task UpdateAsync(UpdateProductCategoryModel updateProductCategoryModel)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/{version}/{nameController}/", updateProductCategoryModel);
        await EnsureSuccessStatusCodeAsync(response);
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/{version}/{nameController}/{id}");
        await EnsureSuccessStatusCodeAsync(response);
    }

    private static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new ApiClientException(response.StatusCode, content);
        }
    }
}