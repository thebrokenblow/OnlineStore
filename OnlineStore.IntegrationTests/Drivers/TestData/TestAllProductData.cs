using Reqnroll.Assist.Attributes;

namespace OnlineStore.IntegrationTests.Drivers.TestData;

public class TestAllProductData(string name, decimal price)
{
    [TableAliases("Name")]
    public string Name { get; set; } = name;

    [TableAliases("Price")]
    public required decimal Price { get; set; } = price;
}