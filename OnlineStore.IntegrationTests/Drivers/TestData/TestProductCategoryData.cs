using Reqnroll.Assist.Attributes;

namespace OnlineStore.IntegrationTests.Drivers.TestData;

public class TestProductCategoryData(
    string name,
    string description)
{
    [TableAliases("Name")]
    public string Name { get; set; } = name;

    [TableAliases("Description")]
    public string Description { get; set; } = description;
}