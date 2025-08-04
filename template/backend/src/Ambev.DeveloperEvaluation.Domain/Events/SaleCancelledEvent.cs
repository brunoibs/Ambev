using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando uma venda é cancelada
/// </summary>
public class SaleCancelledEvent
{
    /// <summary>
    /// Obtém ou define a venda que foi cancelada
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Obtém ou define a data de cancelamento do evento
    /// </summary>
    public DateTime CancelledAt { get; set; }

    /// <summary>
    /// Obtém ou define o ID do usuário que cancelou a venda
    /// </summary>
    public Guid CancelledBy { get; set; }

    /// <summary>
    /// Inicializa uma nova instância do evento SaleCancelledEvent
    /// </summary>
    /// <param name="sale">A venda que foi cancelada</param>
    /// <param name="cancelledBy">ID do usuário que cancelou a venda</param>
    public SaleCancelledEvent(Sale sale, Guid cancelledBy)
    {
        Sale = sale;
        CancelledBy = cancelledBy;
        CancelledAt = DateTime.UtcNow;
    }
} 