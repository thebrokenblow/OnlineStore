using Reqnroll.Assist.Attributes;

namespace OnlineStore.IntegrationTests.Drivers.TestData;

public class TestProductData(
    string name,
    string description,
    decimal price,
    string categoryName
)
{
    [TableAliases("Name")]
    public required string Name { get; set; } = name;

    [TableAliases("Description")]
    public required string Description { get; set; } = description;

    [TableAliases("Price")]
    public required decimal Price { get; set; } = price;

    [TableAliases("CategoryName")]
    public required string CategoryName { get; set; } = categoryName;
}