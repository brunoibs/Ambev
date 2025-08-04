using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Evento disparado quando uma venda é modificada
/// </summary>
public class SaleModifiedEvent
{
    /// <summary>
    /// Obtém ou define a venda que foi modificada
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Obtém ou define a data de modificação do evento
    /// </summary>
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// Obtém ou define o ID do usuário que modificou a venda
    /// </summary>
    public Guid ModifiedBy { get; set; }

    /// <summary>
    /// Inicializa uma nova instância do evento SaleModifiedEvent
    /// </summary>
    /// <param name="sale">A venda que foi modificada</param>
    /// <param name="modifiedBy">ID do usuário que modificou a venda</param>
    public SaleModifiedEvent(Sale sale, Guid modifiedBy)
    {
        Sale = sale;
        ModifiedBy = modifiedBy;
        ModifiedAt = DateTime.UtcNow;
    }
} 