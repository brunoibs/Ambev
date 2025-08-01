using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Product válidas.
    /// Os produtos gerados terão:
    /// - Nome válido (usando nomes de produtos)
    /// - Preço válido (entre 0.01 e 10000.00)
    /// - Quantidade válida (entre 0 e 10000)
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(0.01m, 10000.00m))
        .RuleFor(p => p.Amount, f => f.Random.Int(0, 10000))
        .RuleFor(p => p.Id, f => f.Random.Guid());

    /// <summary>
    /// Gera uma entidade Product válida com dados aleatórios.
    /// O produto gerado terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Uma entidade Product válida com dados gerados aleatoriamente.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Gera um nome válido de produto usando Faker.
    /// O nome gerado será:
    /// - Entre 3 e 100 caracteres
    /// - Usar convenções de nomes de produtos
    /// - Conter apenas caracteres válidos
    /// </summary>
    /// <returns>Um nome válido de produto.</returns>
    public static string GenerateValidProductName()
    {
        return new Faker().Commerce.ProductName();
    }

    /// <summary>
    /// Gera um preço válido para produto.
    /// O preço gerado será:
    /// - Entre 0.01 e 10000.00
    /// - Com no máximo 2 casas decimais
    /// - Valor positivo
    /// </summary>
    /// <returns>Um preço válido para produto.</returns>
    public static decimal GenerateValidPrice()
    {
        return new Faker().Random.Decimal(0.01m, 10000.00m);
    }

    /// <summary>
    /// Gera uma quantidade válida para produto.
    /// A quantidade gerada será:
    /// - Entre 0 e 10000
    /// - Valor inteiro positivo
    /// - Representar estoque realista
    /// </summary>
    /// <returns>Uma quantidade válida para produto.</returns>
    public static int GenerateValidAmount()
    {
        return new Faker().Random.Int(0, 10000);
    }

    /// <summary>
    /// Gera um nome de produto inválido para testar cenários negativos.
    /// O nome gerado será:
    /// - Vazio ou nulo
    /// - Não seguir o formato esperado
    /// - Ser uma string simples ou vazia
    /// Isso é útil para testar casos de erro de validação de nome.
    /// </summary>
    /// <returns>Um nome de produto inválido.</returns>
    public static string? GenerateInvalidProductName()
    {
        var faker = new Faker();
        return faker.PickRandom("", null, "   ", "a", "ab");
    }

    /// <summary>
    /// Gera um preço inválido para produto para testar cenários negativos.
    /// O preço gerado será:
    /// - Negativo
    /// - Zero
    /// - Valor muito alto
    /// Isso é útil para testar casos de erro de validação de preço.
    /// </summary>
    /// <returns>Um preço inválido para produto.</returns>
    public static decimal GenerateInvalidPrice()
    {
        var faker = new Faker();
        return faker.PickRandom(-10.00m, 0.00m, 100000.00m);
    }

    /// <summary>
    /// Gera uma quantidade inválida para produto para testar cenários negativos.
    /// A quantidade gerada será:
    /// - Negativa
    /// - Valor muito alto
    /// - Não representar estoque realista
    /// Isso é útil para testar casos de erro de validação de quantidade.
    /// </summary>
    /// <returns>Uma quantidade inválida para produto.</returns>
    public static int GenerateInvalidAmount()
    {
        var faker = new Faker();
        return faker.PickRandom(-100, -1, 1000000);
    }

    /// <summary>
    /// Gera um nome de produto que excede o limite máximo de caracteres.
    /// O nome gerado será:
    /// - Mais longo que 100 caracteres
    /// - Conter caracteres alfanuméricos aleatórios
    /// Isso é útil para testar casos de erro de validação de comprimento de nome.
    /// </summary>
    /// <returns>Um nome de produto que excede o limite máximo de caracteres.</returns>
    public static string GenerateLongProductName()
    {
        return new Faker().Random.String2(101);
    }

    /// <summary>
    /// Gera um produto com dados mínimos válidos.
    /// O produto gerado terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Um produto com dados mínimos válidos.</returns>
    public static Product GenerateMinimalValidProduct()
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = GenerateValidProductName(),
            Price = GenerateValidPrice(),
            Amount = GenerateValidAmount()
        };
    }

    /// <summary>
    /// Gera um produto com dados inválidos para testar cenários negativos.
    /// O produto gerado terá:
    /// - Nome inválido (vazio, nulo ou muito longo)
    /// - Preço inválido (negativo, zero ou muito alto)
    /// - Quantidade inválida (negativa ou muito alta)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um produto com dados inválidos.</returns>
    public static Product GenerateInvalidProduct()
    {
        return new Product
        {
            Id = Guid.Empty, // Inválido: ID vazio
            Name = GenerateInvalidProductName() ?? string.Empty, // Inválido: nome inválido
            Price = GenerateInvalidPrice(), // Inválido: preço inválido
            Amount = GenerateInvalidAmount() // Inválido: quantidade inválida
        };
    }
} 