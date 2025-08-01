using Ambev.DeveloperEvaluation.Application.Sale.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class ListSaleHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Sale válidas.
    /// As vendas geradas terão:
    /// - Data de venda válida
    /// - Total válido
    /// - Desconto válido
    /// - Cliente válido
    /// - ID único
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.DtSale, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(s => s.Total, f => f.Random.Decimal(10.00m, 1000.00m))
        .RuleFor(s => s.Discount, f => f.Random.Decimal(0.00m, 100.00m))
        .RuleFor(s => s.Cancel, f => f.Random.Bool(0.1f))
        .RuleFor(s => s.DtCreate, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(s => s.DtEdit, f => f.Random.Bool(0.3f) ? f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now) : null)
        .RuleFor(s => s.DtCancel, f => f.Random.Bool(0.1f) ? f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now) : null)
        .RuleFor(s => s.IdCustomer, f => f.Random.Guid())
        .RuleFor(s => s.IdCreate, f => f.Random.Guid())
        .RuleFor(s => s.IdEdit, f => f.Random.Bool(0.3f) ? f.Random.Guid() : null)
        .RuleFor(s => s.IdCancel, f => f.Random.Bool(0.1f) ? f.Random.Guid() : null)
        .RuleFor(s => s.ProductSales, f => new List<ProductSale>());

    /// <summary>
    /// Gera uma query ListSaleQuery válida.
    /// A query gerada não terá parâmetros específicos, pois lista todas as vendas.
    /// </summary>
    /// <returns>Uma query ListSaleQuery válida.</returns>
    public static ListSaleQuery GenerateValidQuery()
    {
        return new ListSaleQuery();
    }

    /// <summary>
    /// Gera uma lista de vendas válidas para simular dados do repositório.
    /// A lista gerada terá:
    /// - Entre 1 e 10 vendas
    /// - Dados válidos para cada venda
    /// - IDs únicos
    /// </summary>
    /// <returns>Uma lista de vendas válidas.</returns>
    public static List<Sale> GenerateValidSales()
    {
        return SaleFaker.GenerateBetween(1, 10);
    }

    /// <summary>
    /// Gera uma lista vazia de vendas para testar cenários sem dados.
    /// </summary>
    /// <returns>Uma lista vazia de vendas.</returns>
    public static List<Sale> GenerateEmptySales()
    {
        return new List<Sale>();
    }

    /// <summary>
    /// Gera uma lista com uma única venda para testar cenários específicos.
    /// </summary>
    /// <returns>Uma lista com uma única venda.</returns>
    public static List<Sale> GenerateSingleSale()
    {
        return new List<Sale> { SaleFaker.Generate() };
    }

    /// <summary>
    /// Gera uma lista com múltiplas vendas para testar cenários com muitos dados.
    /// </summary>
    /// <returns>Uma lista com múltiplas vendas.</returns>
    public static List<Sale> GenerateMultipleSales()
    {
        return SaleFaker.GenerateBetween(5, 15);
    }
} 