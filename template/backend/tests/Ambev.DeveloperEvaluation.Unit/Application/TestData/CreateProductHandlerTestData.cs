using Ambev.DeveloperEvaluation.Application.Product.Create;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar comandos CreateProductCommand válidos.
    /// Os comandos gerados terão:
    /// - Nome válido (usando nomes de produtos)
    /// - Preço válido (entre 0.01 e 10000.00)
    /// - Quantidade válida (entre 0 e 10000)
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<CreateProductCommand> CreateProductCommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(c => c.Name, f => f.Commerce.ProductName())
        .RuleFor(c => c.Price, f => f.Random.Decimal(0.01m, 10000.00m))
        .RuleFor(c => c.Amount, f => f.Random.Int(0, 10000));

    /// <summary>
    /// Gera um comando CreateProductCommand válido com dados aleatórios.
    /// O comando gerado terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Um comando CreateProductCommand válido com dados gerados aleatoriamente.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return CreateProductCommandFaker.Generate();
    }

    /// <summary>
    /// Gera um comando CreateProductCommand com dados inválidos para testar cenários negativos.
    /// O comando gerado terá:
    /// - Nome vazio ou inválido
    /// - Preço inválido (zero ou negativo)
    /// - Quantidade inválida (negativa)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateProductCommand com dados inválidos.</returns>
    public static CreateProductCommand GenerateInvalidCommand()
    {
        return new CreateProductCommand
        {
            Name = string.Empty, // Inválido: nome vazio
            Price = 0.00m, // Inválido: preço zero
            Amount = -1 // Inválido: quantidade negativa
        };
    }

    /// <summary>
    /// Gera um comando CreateProductCommand com nome muito longo para testar cenários negativos.
    /// O comando gerado terá:
    /// - Nome que excede o limite máximo de caracteres
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateProductCommand com nome muito longo.</returns>
    public static CreateProductCommand GenerateCommandWithLongName()
    {
        return new CreateProductCommand
        {
            Name = new Faker().Random.String2(101), // Inválido: nome muito longo
            Price = 10.00m,
            Amount = 5
        };
    }

    /// <summary>
    /// Gera um comando CreateProductCommand com preço zero para testar cenários negativos.
    /// O comando gerado terá:
    /// - Preço zero (inválido)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateProductCommand com preço zero.</returns>
    public static CreateProductCommand GenerateCommandWithZeroPrice()
    {
        return new CreateProductCommand
        {
            Name = "Produto Teste",
            Price = 0.00m, // Inválido: preço zero
            Amount = 1
        };
    }

    /// <summary>
    /// Gera um comando CreateProductCommand com quantidade negativa para testar cenários negativos.
    /// O comando gerado terá:
    /// - Quantidade negativa (inválida)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateProductCommand com quantidade negativa.</returns>
    public static CreateProductCommand GenerateCommandWithNegativeAmount()
    {
        return new CreateProductCommand
        {
            Name = "Produto Teste",
            Price = 10.00m,
            Amount = -1 // Inválido: quantidade negativa
        };
    }

    /// <summary>
    /// Gera um comando CreateProductCommand com dados mínimos válidos.
    /// O comando gerado terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Um comando CreateProductCommand com dados mínimos válidos.</returns>
    public static CreateProductCommand GenerateMinimalValidCommand()
    {
        return new CreateProductCommand
        {
            Name = "Produto Teste",
            Price = 1.00m,
            Amount = 1
        };
    }
} 