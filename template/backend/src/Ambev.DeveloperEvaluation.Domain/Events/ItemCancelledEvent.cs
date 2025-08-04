using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando um item de venda é cancelado
/// </summary>
public class ItemCancelledEvent
{
    /// <summary>
    /// Obtém ou define o item de venda que foi cancelado
    /// </summary>
    public ProductSale ProductSale { get; set; }

    /// <summary>
    /// Obtém ou define a venda à qual o item pertence
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Obtém ou define a data de cancelamento do evento
    /// </summary>
    public DateTime CancelledAt { get; set; }

    /// <summary>
    /// Obtém ou define o ID do usuário que cancelou o item
    /// </summary>
    public Guid CancelledBy { get; set; }

    /// <summary>
    /// Inicializa uma nova instância do evento ItemCancelledEvent
    /// </summary>
    /// <param name="productSale">O item de venda que foi cancelado</param>
    /// <param name="sale">A venda à qual o item pertence</param>
    /// <param name="cancelledBy">ID do usuário que cancelou o item</param>
    public ItemCancelledEvent(ProductSale productSale, Sale sale, Guid cancelledBy)
    {
        ProductSale = productSale;
        Sale = sale;
        CancelledBy = cancelledBy;
        CancelledAt = DateTime.UtcNow;
    }
} 