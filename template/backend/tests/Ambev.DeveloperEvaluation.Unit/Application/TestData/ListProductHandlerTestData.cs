using Ambev.DeveloperEvaluation.Application.Product.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class ListProductHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Product válidas.
    /// Os produtos gerados terão:
    /// - Nome válido (usando nomes de produtos)
    /// - Preço válido (entre 0.01 e 10000.00)
    /// - Quantidade válida (entre 0 e 10000)
    /// - ID único
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(0.01m, 10000.00m))
        .RuleFor(p => p.Amount, f => f.Random.Int(0, 10000));

    /// <summary>
    /// Gera uma query ListProductQuery válida.
    /// A query gerada não terá parâmetros específicos, pois lista todos os produtos.
    /// </summary>
    /// <returns>Uma query ListProductQuery válida.</returns>
    public static ListProductQuery GenerateValidQuery()
    {
        return new ListProductQuery();
    }

    /// <summary>
    /// Gera uma lista de produtos válidos para simular dados do repositório.
    /// A lista gerada terá:
    /// - Entre 1 e 10 produtos
    /// - Dados válidos para cada produto
    /// - IDs únicos
    /// </summary>
    /// <returns>Uma lista de produtos válidos.</returns>
    public static List<Product> GenerateValidProducts()
    {
        return ProductFaker.GenerateBetween(1, 10);
    }

    /// <summary>
    /// Gera uma lista vazia de produtos para testar cenários sem dados.
    /// </summary>
    /// <returns>Uma lista vazia de produtos.</returns>
    public static List<Product> GenerateEmptyProducts()
    {
        return new List<Product>();
    }

    /// <summary>
    /// Gera uma lista com um único produto para testar cenários específicos.
    /// </summary>
    /// <returns>Uma lista com um único produto.</returns>
    public static List<Product> GenerateSingleProduct()
    {
        return new List<Product> { ProductFaker.Generate() };
    }

    /// <summary>
    /// Gera uma lista com múltiplos produtos para testar cenários com muitos dados.
    /// </summary>
    /// <returns>Uma lista com múltiplos produtos.</returns>
    public static List<Product> GenerateMultipleProducts()
    {
        return ProductFaker.GenerateBetween(5, 15);
    }
} 