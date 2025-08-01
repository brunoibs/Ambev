using Ambev.DeveloperEvaluation.Application.Product.Delete;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class DeleteProductHandlerTestData
{
    /// <summary>
    /// Gera um comando DeleteProductCommand válido com ID aleatório.
    /// O comando gerado terá um ID válido para deletar um produto.
    /// </summary>
    /// <returns>Um comando DeleteProductCommand válido com ID gerado aleatoriamente.</returns>
    public static DeleteProductCommand GenerateValidCommand()
    {
        return new DeleteProductCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Gera um comando DeleteProductCommand com ID específico.
    /// O comando gerado terá um ID específico para testar cenários específicos.
    /// </summary>
    /// <param name="id">O ID específico para o comando.</param>
    /// <returns>Um comando DeleteProductCommand com ID específico.</returns>
    public static DeleteProductCommand GenerateCommandWithSpecificId(Guid id)
    {
        return new DeleteProductCommand(id);
    }

    /// <summary>
    /// Gera um comando DeleteProductCommand com ID vazio para testar cenários negativos.
    /// O comando gerado terá um ID vazio que não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteProductCommand com ID vazio.</returns>
    public static DeleteProductCommand GenerateCommandWithEmptyId()
    {
        return new DeleteProductCommand(Guid.Empty);
    }

    /// <summary>
    /// Gera um comando DeleteProductCommand com ID aleatório para testar cenários negativos.
    /// O comando gerado terá um ID que provavelmente não existe no sistema.
    /// </summary>
    /// <returns>Um comando DeleteProductCommand com ID que não existe.</returns>
    public static DeleteProductCommand GenerateCommandWithNonExistentId()
    {
        return new DeleteProductCommand(Guid.NewGuid());
    }
} 