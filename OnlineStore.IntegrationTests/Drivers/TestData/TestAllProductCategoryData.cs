using Reqnroll.Assist.Attributes;

namespace OnlineStore.IntegrationTests.Drivers.TestData;

public class TestAllProductCategoryData(string name)
{
    [TableAliases("Name")]
    public string Name { get; set; } = name;
}