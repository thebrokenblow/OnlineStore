﻿using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommand : IRequest<int>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}