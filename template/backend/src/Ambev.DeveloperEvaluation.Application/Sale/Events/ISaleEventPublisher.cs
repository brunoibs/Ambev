using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

/// <summary>
/// Interface para publicar eventos de venda
/// </summary>
public interface ISaleEventPublisher
{
    /// <summary>
    /// Publica o evento de venda criada
    /// </summary>
    /// <param name="sale">A venda que foi criada</param>
    Task PublishSaleCreatedAsync(SaleCreatedEvent saleCreatedEvent);

    /// <summary>
    /// Publica o evento de venda modificada
    /// </summary>
    /// <param name="saleModifiedEvent">O evento de venda modificada</param>
    Task PublishSaleModifiedAsync(SaleModifiedEvent saleModifiedEvent);

    /// <summary>
    /// Publica o evento de venda cancelada
    /// </summary>
    /// <param name="saleCancelledEvent">O evento de venda cancelada</param>
    Task PublishSaleCancelledAsync(SaleCancelledEvent saleCancelledEvent);

    /// <summary>
    /// Publica o evento de item cancelado
    /// </summary>
    /// <param name="itemCancelledEvent">O evento de item cancelado</param>
    Task PublishItemCancelledAsync(ItemCancelledEvent itemCancelledEvent);
} 