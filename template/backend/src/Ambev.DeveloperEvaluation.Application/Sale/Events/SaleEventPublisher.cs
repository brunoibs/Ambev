using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events;

/// <summary>
/// Implementação do publicador de eventos de venda
/// </summary>
public class SaleEventPublisher : ISaleEventPublisher
{
    private readonly ILogger<SaleEventPublisher> _logger;

    /// <summary>
    /// Inicializa uma nova instância do publicador de eventos
    /// </summary>
    /// <param name="logger">Logger para registrar os eventos</param>
    public SaleEventPublisher(ILogger<SaleEventPublisher> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Publica o evento de venda criada
    /// </summary>
    /// <param name="saleCreatedEvent">O evento de venda criada</param>
    public async Task PublishSaleCreatedAsync(SaleCreatedEvent saleCreatedEvent)
    {
        _logger.LogInformation(
            "EVENTO: SaleCreated - Venda {SaleId} criada em {CreatedAt} pelo usuário {UserId}",
            saleCreatedEvent.Sale.Id,
            saleCreatedEvent.CreatedAt,
            saleCreatedEvent.Sale.IdCreate);

        // Aqui seria implementada a publicação para um Message Broker
        // Por exemplo: await _messageBroker.PublishAsync("sale.created", saleCreatedEvent);
        
        await Task.CompletedTask;
    }

    /// <summary>
    /// Publica o evento de venda modificada
    /// </summary>
    /// <param name="saleModifiedEvent">O evento de venda modificada</param>
    public async Task PublishSaleModifiedAsync(SaleModifiedEvent saleModifiedEvent)
    {
        _logger.LogInformation(
            "EVENTO: SaleModified - Venda {SaleId} modificada em {ModifiedAt} pelo usuário {UserId}",
            saleModifiedEvent.Sale.Id,
            saleModifiedEvent.ModifiedAt,
            saleModifiedEvent.ModifiedBy);

        // Aqui seria implementada a publicação para um Message Broker
        // Por exemplo: await _messageBroker.PublishAsync("sale.modified", saleModifiedEvent);
        
        await Task.CompletedTask;
    }

    /// <summary>
    /// Publica o evento de venda cancelada
    /// </summary>
    /// <param name="saleCancelledEvent">O evento de venda cancelada</param>
    public async Task PublishSaleCancelledAsync(SaleCancelledEvent saleCancelledEvent)
    {
        _logger.LogInformation(
            "EVENTO: SaleCancelled - Venda {SaleId} cancelada em {CancelledAt} pelo usuário {UserId}",
            saleCancelledEvent.Sale.Id,
            saleCancelledEvent.CancelledAt,
            saleCancelledEvent.CancelledBy);

        // Aqui seria implementada a publicação para um Message Broker
        // Por exemplo: await _messageBroker.PublishAsync("sale.cancelled", saleCancelledEvent);
        
        await Task.CompletedTask;
    }

    /// <summary>
    /// Publica o evento de item cancelado
    /// </summary>
    /// <param name="itemCancelledEvent">O evento de item cancelado</param>
    public async Task PublishItemCancelledAsync(ItemCancelledEvent itemCancelledEvent)
    {
        _logger.LogInformation(
            "EVENTO: ItemCancelled - Item {ProductSaleId} da venda {SaleId} cancelado em {CancelledAt} pelo usuário {UserId}",
            itemCancelledEvent.ProductSale.Id,
            itemCancelledEvent.Sale.Id,
            itemCancelledEvent.CancelledAt,
            itemCancelledEvent.CancelledBy);

        // Aqui seria implementada a publicação para um Message Broker
        // Por exemplo: await _messageBroker.PublishAsync("item.cancelled", itemCancelledEvent);
        
        await Task.CompletedTask;
    }
} 