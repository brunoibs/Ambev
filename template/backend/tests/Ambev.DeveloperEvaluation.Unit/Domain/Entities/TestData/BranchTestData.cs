using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class BranchTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Branch válidas.
    /// As filiais geradas terão:
    /// - Nome válido (usando nomes de empresas)
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Id, f => f.Random.Guid());

    /// <summary>
    /// Gera uma entidade Branch válida com dados aleatórios.
    /// A filial gerada terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Uma entidade Branch válida com dados gerados aleatoriamente.</returns>
    public static Branch GenerateValidBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Gera um nome válido de filial usando Faker.
    /// O nome gerado será:
    /// - Entre 3 e 100 caracteres
    /// - Usar convenções de nomes de empresas
    /// - Conter apenas caracteres válidos
    /// </summary>
    /// <returns>Um nome válido de filial.</returns>
    public static string GenerateValidBranchName()
    {
        return new Faker().Company.CompanyName();
    }

    /// <summary>
    /// Gera um nome de filial inválido para testar cenários negativos.
    /// O nome gerado será:
    /// - Vazio ou nulo
    /// - Não seguir o formato esperado
    /// - Ser uma string simples ou vazia
    /// Isso é útil para testar casos de erro de validação de nome.
    /// </summary>
    /// <returns>Um nome de filial inválido.</returns>
    public static string? GenerateInvalidBranchName()
    {
        var faker = new Faker();
        return faker.PickRandom("", null, "   ", "a", "ab");
    }

    /// <summary>
    /// Gera um nome de filial que excede o limite máximo de caracteres.
    /// O nome gerado será:
    /// - Mais longo que 100 caracteres
    /// - Conter caracteres alfanuméricos aleatórios
    /// Isso é útil para testar casos de erro de validação de comprimento de nome.
    /// </summary>
    /// <returns>Um nome de filial que excede o limite máximo de caracteres.</returns>
    public static string GenerateLongBranchName()
    {
        return new Faker().Random.String2(101);
    }

    /// <summary>
    /// Gera uma filial com dados mínimos válidos.
    /// A filial gerada terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Uma filial com dados mínimos válidos.</returns>
    public static Branch GenerateMinimalValidBranch()
    {
        return new Branch
        {
            Id = Guid.NewGuid(),
            Name = GenerateValidBranchName()
        };
    }

    /// <summary>
    /// Gera uma filial com dados inválidos para testar cenários negativos.
    /// A filial gerada terá:
    /// - Nome inválido (vazio, nulo ou muito longo)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Uma filial com dados inválidos.</returns>
    public static Branch GenerateInvalidBranch()
    {
        return new Branch
        {
            Id = Guid.Empty, // Inválido: ID vazio
            Name = GenerateInvalidBranchName() ?? string.Empty // Inválido: nome inválido
        };
    }
} 