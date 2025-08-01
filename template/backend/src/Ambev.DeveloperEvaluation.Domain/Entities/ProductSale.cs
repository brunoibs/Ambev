using Ambev.DeveloperEvaluation.Domain.Common;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa o relacionamento entre uma venda e um produto (item de uma venda).
/// </summary>
public class ProductSale : BaseEntity
{
    /// <summary>
    /// Obtém ou define o Id da venda.
    /// </summary>
    public Guid IdSale { get; set; }

    /// <summary>
    /// Obtém ou define o Id do produto.
    /// </summary>
    public Guid IdProduct { get; set; }

    /// <summary>
    /// Obtém ou define a quantidade do produto na venda.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Obtém ou define o preço do produto na venda.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Obtém ou define o valor total deste item na venda.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe ProductSale.
    /// </summary>
    public ProductSale() { }
} 