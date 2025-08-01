using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;

/// <summary>
/// Request para deletar uma venda
/// </summary>
public class DeleteSaleRequest
{
    /// <summary>
    /// ID da venda
    /// </summary>
    [Required]
    public Guid Id { get; set; }
} 