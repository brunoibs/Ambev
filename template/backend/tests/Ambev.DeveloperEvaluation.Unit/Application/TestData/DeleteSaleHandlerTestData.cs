using Ambev.DeveloperEvaluation.Application.Sale.Delete;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class DeleteSaleHandlerTestData
{
    /// <summary>
    /// Gera um comando DeleteSaleCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para deletar uma venda.
    /// </summary>
    /// <returns>Um comando DeleteSaleCommand válido com ID gerado aleatoriamente.</returns>
    public static DeleteSaleCommand GenerateValidCommand()
    {
        return new DeleteSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando DeleteSaleCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando DeleteSaleCommand com ID específico.</returns>
    public static DeleteSaleCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new DeleteSaleCommand(id);
    }

    /// <summary>
    /// Gera um comando DeleteSaleCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteSaleCommand com ID vazio.</returns>
    public static DeleteSaleCommand GenerateCommandWithEmptyId()
    {
        return new DeleteSaleCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando DeleteSaleCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteSaleCommand com ID que não existe.</returns>
    public static DeleteSaleCommand GenerateCommandWithNonExistentId()
    {
        return new DeleteSaleCommand(Guid.NewGuid());
    }
} 