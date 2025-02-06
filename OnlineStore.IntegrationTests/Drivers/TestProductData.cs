using Reqnroll.Assist.Attributes;

namespace OnlineStore.IntegrationTests.Drivers;

public class TestProductData(
    int id,
    string name,
    string description,
    decimal price,
    int idProductCategory
)
{
    [TableAliases("Id")]
    public int Id { get; set; } = id;

    [TableAliases("Name")]
    public required string Name { get; set; } = name;

    [TableAliases("Description")]
    public required string Description { get; set; } = description;

    [TableAliases("Price")]
    public required decimal Price { get; set; } = price;

    [TableAliases("IdProductCategory")]
    public required int IdProductCategory { get; set; } = idProductCategory;
}