using Ambev.DeveloperEvaluation.Application.Branch.Create;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class CreateBranchHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar comandos CreateBranchCommand válidos.
    /// Os comandos gerados terão:
    /// - Nome válido (usando nomes de empresas)
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<CreateBranchCommand> CreateBranchCommandFaker = new Faker<CreateBranchCommand>()
        .RuleFor(c => c.Name, f => f.Company.CompanyName());

    /// <summary>
    /// Gera um comando CreateBranchCommand válido com dados aleatórios.
    /// O comando gerado terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Um comando CreateBranchCommand válido com dados gerados aleatoriamente.</returns>
    public static CreateBranchCommand GenerateValidCommand()
    {
        return CreateBranchCommandFaker.Generate();
    }

    /// <summary>
    /// Gera um comando CreateBranchCommand com nome inválido para testar cenários negativos.
    /// O comando gerado terá:
    /// - Nome vazio ou nulo
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateBranchCommand com dados inválidos.</returns>
    public static CreateBranchCommand GenerateInvalidCommand()
    {
        return new CreateBranchCommand
        {
            Name = string.Empty // Inválido: nome vazio
        };
    }

    /// <summary>
    /// Gera um comando CreateBranchCommand com nome muito longo para testar cenários negativos.
    /// O comando gerado terá:
    /// - Nome que excede o limite máximo de caracteres
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateBranchCommand com nome muito longo.</returns>
    public static CreateBranchCommand GenerateCommandWithLongName()
    {
        return new CreateBranchCommand
        {
            Name = new Faker().Random.String2(101) // Inválido: nome muito longo
        };
    }

    /// <summary>
    /// Gera um comando CreateBranchCommand com nome mínimo válido.
    /// O comando gerado terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Um comando CreateBranchCommand com dados mínimos válidos.</returns>
    public static CreateBranchCommand GenerateMinimalValidCommand()
    {
        return new CreateBranchCommand
        {
            Name = "Filial Teste"
        };
    }
} 