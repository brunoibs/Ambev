namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// Response para busca de venda
/// </summary>
public class GetSaleResponse
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
    /// Data de criação
    /// </summary>
    public DateTime DtCreate { get; set; }

    /// <summary>
    /// Data de edição
    /// </summary>
    public DateTime? DtEdit { get; set; }

    /// <summary>
    /// Data de cancelamento
    /// </summary>
    public DateTime? DtCancel { get; set; }

    /// <summary>
    /// ID do cliente
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// ID do usuário que criou a venda
    /// </summary>
    public Guid IdCreate { get; set; }

    /// <summary>
    /// ID do usuário que editou a venda
    /// </summary>
    public Guid? IdEdit { get; set; }

    /// <summary>
    /// ID do usuário que cancelou a venda
    /// </summary>
    public Guid? IdCancel { get; set; }
} 