namespace Ambev.DeveloperEvaluation.Application.Product.Get;

public class GetProductResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
} 