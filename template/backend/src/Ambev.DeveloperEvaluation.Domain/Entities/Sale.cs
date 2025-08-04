using Ambev.DeveloperEvaluation.Domain.Common;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa uma venda no sistema.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Obtém ou define a data da venda.
    /// </summary>
    public DateTime DtSale { get; set; }

    /// <summary>
    /// Obtém ou define o valor total da venda.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Obtém ou define o desconto aplicado à venda.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Obtém ou define se a venda está cancelada.
    /// </summary>
    public bool Cancel { get; set; } = false;

    /// <summary>
    /// Obtém ou define a data de criação da venda.
    /// </summary>
    public DateTime DtCreate { get; set; }

    /// <summary>
    /// Obtém ou define a data da última edição da venda.
    /// </summary>
    public DateTime? DtEdit { get; set; }

    /// <summary>
    /// Obtém ou define a data de cancelamento da venda.
    /// </summary>
    public DateTime? DtCancel { get; set; }

    /// <summary>
    /// Obtém ou define o Id do usuário cliente.
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// Obtém ou define o Id do usuário que criou a venda.
    /// </summary>
    public Guid IdCreate { get; set; }

    /// <summary>
    /// Obtém ou define o Id do usuário que editou a venda.
    /// </summary>
    public Guid? IdEdit { get; set; }

    /// <summary>
    /// Obtém ou define o Id do usuário que cancelou a venda.
    /// </summary>
    public Guid? IdCancel { get; set; }

    /// <summary>
    /// Obtém ou define o Id da filial onde a venda foi realizada.
    /// </summary>
    public Guid IdBranch { get; set; }

    public virtual List<ProductSale> ProductSales { get; set; } = new List<ProductSale>();

    /// <summary>
    /// Inicializa uma nova instância da classe Sale.
    /// </summary>
    public Sale() { }
} 