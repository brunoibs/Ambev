using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// Request para buscar uma venda
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// ID da venda
    /// </summary>
    [Required]
    public Guid Id { get; set; }
} 