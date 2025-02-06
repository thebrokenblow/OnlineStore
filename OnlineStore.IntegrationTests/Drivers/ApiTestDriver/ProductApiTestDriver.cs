using Newtonsoft.Json;
using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineShop.WebApi.Model.Product;
using OnlineStore.IntegrationTests.Fixture;
using System.Net.Http.Json;

namespace OnlineStore.IntegrationTests.Drivers.ApiTestDriver;

public class ProductApiTestDriver(ITestServerFixture fixture)
{
    private const string nameController = "products";
    private readonly HttpClient _httpClient = fixture.HttpClient;

    public async Task<List<AllProductDto>> GetAll()
    {
        var response = await _httpClient.GetAsync($"/api/{nameController}");
        await EnsureSuccessStatusCode(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<AllProductDto>>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<List<RangeProductDto>> GetRange(int countSkip, int countTake)
    {
        var response = await _httpClient.GetAsync($"/api/{nameController}/{countSkip}/{countTake}");
        await EnsureSuccessStatusCode(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<RangeProductDto>>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<DetailsProductDto> GetDetails(int id)
    {
        var response = await _httpClient.GetAsync($"/api/{nameController}/{id}");
        await EnsureSuccessStatusCode(response);

        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<DetailsProductDto>(content)
               ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }

    public async Task<int> AddProduct(CreateProductModel createProductModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/{nameController}", createProductModel);
        await EnsureSuccessStatusCode(response);

        string content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AddProductResult>(content)
                                  ?? throw new FormatException($"Unexpected response: {content}");

        return result.Id;
    }

    public async Task UpdateProduct(UpdateProductModel updateProductModel)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/{nameController}/", updateProductModel);
        await EnsureSuccessStatusCode(response);
    }

    public async Task DeleteProduct(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/{nameController}/{id}");
        await EnsureSuccessStatusCode(response);
    }

    private static async Task EnsureSuccessStatusCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new ApiClientException(response.StatusCode, content);
        }
    }

    private record AddProductResult(int Id);
}