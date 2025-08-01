using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa um produto no sistema.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Obtém ou define o nome do produto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o preço do produto.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Obtém ou define a quantidade disponível do produto.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe Product.
    /// </summary>
    public Product() { }
} 