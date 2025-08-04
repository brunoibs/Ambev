using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando uma venda é criada
/// </summary>
public class SaleCreatedEvent
{
    /// <summary>
    /// Obtém ou define a venda que foi criada
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Obtém ou define a data de criação do evento
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Inicializa uma nova instância do evento SaleCreatedEvent
    /// </summary>
    /// <param name="sale">A venda que foi criada</param>
    public SaleCreatedEvent(Sale sale)
    {
        Sale = sale;
        CreatedAt = DateTime.UtcNow;
    }
} 