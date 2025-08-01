using Ambev.DeveloperEvaluation.Application.Branch.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class ListBranchHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Branch válidas.
    /// As filiais geradas terão:
    /// - Nome válido (usando nomes de empresas)
    /// - ID único
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName());

    /// <summary>
    /// Gera uma query ListBranchQuery válida.
    /// A query gerada não terá parâmetros específicos, pois lista todas as filiais.
    /// </summary>
    /// <returns>Uma query ListBranchQuery válida.</returns>
    public static ListBranchQuery GenerateValidQuery()
    {
        return new ListBranchQuery();
    }

    /// <summary>
    /// Gera uma lista de filiais válidas para simular dados do repositório.
    /// A lista gerada terá:
    /// - Entre 1 e 10 filiais
    /// - Dados válidos para cada filial
    /// - IDs únicos
    /// </summary>
    /// <returns>Uma lista de filiais válidas.</returns>
    public static List<Branch> GenerateValidBranches()
    {
        return BranchFaker.GenerateBetween(1, 10);
    }

    /// <summary>
    /// Gera uma lista vazia de filiais para testar cenários sem dados.
    /// </summary>
    /// <returns>Uma lista vazia de filiais.</returns>
    public static List<Branch> GenerateEmptyBranches()
    {
        return new List<Branch>();
    }

    /// <summary>
    /// Gera uma lista com uma única filial para testar cenários específicos.
    /// </summary>
    /// <returns>Uma lista com uma única filial.</returns>
    public static List<Branch> GenerateSingleBranch()
    {
        return new List<Branch> { BranchFaker.Generate() };
    }

    /// <summary>
    /// Gera uma lista com múltiplas filiais para testar cenários com muitos dados.
    /// </summary>
    /// <returns>Uma lista com múltiplas filiais.</returns>
    public static List<Branch> GenerateMultipleBranches()
    {
        return BranchFaker.GenerateBetween(5, 15);
    }
} 