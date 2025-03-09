using Newtonsoft.Json;
using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineShop.WebApi.Model.Product;
using OnlineStore.IntegrationTests.Fixture;
using System.Net.Http.Json;

namespace OnlineStore.IntegrationTests.Drivers.ApiTestDriver.V1;

public class ProductApiTestDriver(ITestServerFixture fixture)
{
    private const string nameController = "products";
    private readonly HttpClient _httpClient = fixture.HttpClient;
    private const string version = "v1";

    public async Task<List<AllProductDto>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync($"/api/{version}/{nameController}");
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<AllProductDto>>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<List<RangeProductDto>> GetRangeAsync(int countSkip, int countTake)
    {
        var response = await _httpClient.GetAsync($"/api/{version}/{nameController}/{countSkip}/{countTake}");
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<RangeProductDto>>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<DetailsProductDto> GetDetailsAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/{version}/{nameController}/{id}");
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<DetailsProductDto>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<int> AddAsync(CreateProductModel createProductModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/{version}/{nameController}", createProductModel);
        await EnsureSuccessStatusCodeAsync(response);

        string content = await response.Content.ReadAsStringAsync();
        var id = int.Parse(content);

        return id;
    }

    public async Task UpdateAsync(UpdateProductModel updateProductModel)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/{version}/{nameController}/", updateProductModel);
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

    private record AddProductResult(int Id);
}