namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSale;

/// <summary>
/// Response para listagem de vendas
/// </summary>
public class ListSaleResponse
{
    /// <summary>
    /// Lista de vendas
    /// </summary>
    public List<SaleListItemResponse> Sales { get; set; } = new List<SaleListItemResponse>();
}

/// <summary>
/// Item de venda para listagem
/// </summary>
public class SaleListItemResponse
{
    /// <summary>
    /// ID da venda
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
    /// Se a venda está cancelada
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// ID do cliente
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// ID do usuário que criou a venda
    /// </summary>
    public Guid IdCreate { get; set; }
} 