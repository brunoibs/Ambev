using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Product.List;

public class ListProductResult
{
    public List<ProductListItem> Products { get; set; } = new();
}

public class ProductListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
} 