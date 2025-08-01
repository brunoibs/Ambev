using Ambev.DeveloperEvaluation.Application.Product.Get;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class GetProductHandlerTestData
{
    /// <summary>
    /// Gera um comando GetProductCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para buscar um produto.
    /// </summary>
    /// <returns>Um comando GetProductCommand válido com ID gerado aleatoriamente.</returns>
    public static GetProductCommand GenerateValidCommand()
    {
        return new GetProductCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando GetProductCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando GetProductCommand com ID específico.</returns>
    public static GetProductCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new GetProductCommand(id);
    }

    /// <summary>
    /// Gera um comando GetProductCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetProductCommand com ID vazio.</returns>
    public static GetProductCommand GenerateCommandWithEmptyId()
    {
        return new GetProductCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando GetProductCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetProductCommand com ID que não existe.</returns>
    public static GetProductCommand GenerateCommandWithNonExistentId()
    {
        return new GetProductCommand(Guid.NewGuid());
    }
} 