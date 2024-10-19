using AutoMapper;
using OnlineShop.Domain;
using OnlineShop.Application.Mappings;

namespace OnlineShop.Application.Products.Queries.GetProductId;

public class GetProductIdVM : IMapWith<Product>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, GetProductIdVM>()
            .ForMember(client => client.Id,
                opt => opt.MapFrom(client => client.Id))
            .ForMember(client => client.Name,
                opt => opt.MapFrom(client => client.Name))
            .ForMember(client => client.Description,
                opt => opt.MapFrom(client => client.Description))
            .ForMember(client => client.Price,
                opt => opt.MapFrom(client => client.Price));
    }
}