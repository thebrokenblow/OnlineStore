﻿namespace OnlineShop.WebApi.Model.ProductCategory;

public class UpdateProductCategoryModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}