using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Request para criar uma nova venda
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Data da venda
    /// </summary>
    [Required]
    public DateTime DtSale { get; set; }

    /// <summary>
    /// ID do cliente (usuário)
    /// </summary>
    [Required]
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// ID do usuário que está criando a venda
    /// </summary>
    [Required]
    public Guid IdCreate { get; set; }

    /// <summary>
    /// ID da filial onde a venda será realizada
    /// </summary>
    [Required]
    public Guid IdBranch { get; set; }

    /// <summary>
    /// Lista de itens da venda (ProductSales)
    /// </summary>
    [Required]
    public List<ProductSaleRequest> ProductSales { get; set; } = new List<ProductSaleRequest>();
}

/// <summary>
/// Request para um item de venda (ProductSale)
/// </summary>
public class ProductSaleRequest
{
    /// <summary>
    /// ID do produto
    /// </summary>
    [Required]
    public Guid IdProduct { get; set; }

    /// <summary>
    /// Quantidade do produto
    /// </summary>
    [Required]
    [Range(1, 20, ErrorMessage = "A quantidade deve estar entre 1 e 20")]
    public int Amount { get; set; }

    /// <summary>
    /// Preço do produto na venda
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal Price { get; set; }

    /// <summary>
    /// Total do item (Preço x Quantidade)
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O total deve ser maior que zero")]
    public decimal Total { get; set; }
} 