using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Representa uma filial no sistema.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Obtém ou define o nome da filial.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Inicializa uma nova instância da classe Branch.
    /// </summary>
    public Branch() { }
} 