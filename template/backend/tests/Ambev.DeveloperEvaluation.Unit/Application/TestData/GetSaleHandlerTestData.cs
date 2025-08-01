using Ambev.DeveloperEvaluation.Application.Sale.Get;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class GetSaleHandlerTestData
{
    /// <summary>
    /// Gera um comando GetSaleCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para buscar uma venda.
    /// </summary>
    /// <returns>Um comando GetSaleCommand válido com ID gerado aleatoriamente.</returns>
    public static GetSaleCommand GenerateValidCommand()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando GetSaleCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando GetSaleCommand com ID específico.</returns>
    public static GetSaleCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new GetSaleCommand(id);
    }

    /// <summary>
    /// Gera um comando GetSaleCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetSaleCommand com ID vazio.</returns>
    public static GetSaleCommand GenerateCommandWithEmptyId()
    {
        return new GetSaleCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando GetSaleCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetSaleCommand com ID que não existe.</returns>
    public static GetSaleCommand GenerateCommandWithNonExistentId()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }
} 