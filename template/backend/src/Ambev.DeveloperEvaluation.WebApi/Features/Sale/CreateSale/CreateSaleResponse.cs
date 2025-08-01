namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Response para criação de venda
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// ID da venda criada
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data da venda
    /// </summary>
    public DateTime DtSale { get; set; }

    /// <summary>
    /// Total da venda
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Desconto aplicado
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// ID do cliente
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// ID do usuário que criou a venda
    /// </summary>
    public Guid IdCreate { get; set; }
} 