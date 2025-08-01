using Ambev.DeveloperEvaluation.Application.Branch.Delete;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class DeleteBranchHandlerTestData
{
    /// <summary>
    /// Gera um comando DeleteBranchCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para deletar uma filial.
    /// </summary>
    /// <returns>Um comando DeleteBranchCommand válido com ID gerado aleatoriamente.</returns>
    public static DeleteBranchCommand GenerateValidCommand()
    {
        return new DeleteBranchCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando DeleteBranchCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando DeleteBranchCommand com ID específico.</returns>
    public static DeleteBranchCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new DeleteBranchCommand(id);
    }

    /// <summary>
    /// Gera um comando DeleteBranchCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteBranchCommand com ID vazio.</returns>
    public static DeleteBranchCommand GenerateCommandWithEmptyId()
    {
        return new DeleteBranchCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando DeleteBranchCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteBranchCommand com ID que não existe.</returns>
    public static DeleteBranchCommand GenerateCommandWithNonExistentId()
    {
        return new DeleteBranchCommand(Guid.NewGuid());
    }
} 