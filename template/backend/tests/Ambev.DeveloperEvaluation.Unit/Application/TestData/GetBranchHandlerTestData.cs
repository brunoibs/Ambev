using Ambev.DeveloperEvaluation.Application.Branch.Get;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class GetBranchHandlerTestData
{
    /// <summary>
    /// Gera um comando GetBranchCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para buscar uma filial.
    /// </summary>
    /// <returns>Um comando GetBranchCommand válido com ID gerado aleatoriamente.</returns>
    public static GetBranchCommand GenerateValidCommand()
    {
        return new GetBranchCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando GetBranchCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando GetBranchCommand com ID específico.</returns>
    public static GetBranchCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new GetBranchCommand(id);
    }

    /// <summary>
    /// Gera um comando GetBranchCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetBranchCommand com ID vazio.</returns>
    public static GetBranchCommand GenerateCommandWithEmptyId()
    {
        return new GetBranchCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando GetBranchCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando GetBranchCommand com ID que não existe.</returns>
    public static GetBranchCommand GenerateCommandWithNonExistentId()
    {
        return new GetBranchCommand(Guid.NewGuid());
    }
} 