using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Configura o Faker para gerar entidades Sale válidas.
    /// As vendas geradas terão:
    /// - Data de venda válida (entre 2020 e 2030)
    /// - Valor total válido (entre 0.01 e 100000.00)
    /// - Desconto válido (entre 0.00 e 50% do total)
    /// - Status de cancelamento válido
    /// - Datas de criação, edição e cancelamento válidas
    /// - IDs de usuários válidos
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.DtSale, f => f.Date.Between(DateTime.Now.AddYears(-3), DateTime.Now))
        .RuleFor(s => s.Total, f => f.Random.Decimal(0.01m, 100000.00m))
        .RuleFor(s => s.Discount, f => f.Random.Decimal(0.00m, 50.00m))
        .RuleFor(s => s.Cancel, f => f.Random.Bool(0.1f)) // 10% de chance de estar cancelada
        .RuleFor(s => s.DtCreate, f => f.Date.Between(DateTime.Now.AddYears(-3), DateTime.Now))
        .RuleFor(s => s.DtEdit, f => f.Random.Bool(0.3f) ? f.Date.Between(DateTime.Now.AddYears(-3), DateTime.Now) : null)
        .RuleFor(s => s.DtCancel, f => f.Random.Bool(0.1f) ? f.Date.Between(DateTime.Now.AddYears(-3), DateTime.Now) : null)
        .RuleFor(s => s.IdCustomer, f => f.Random.Guid())
        .RuleFor(s => s.IdCreate, f => f.Random.Guid())
        .RuleFor(s => s.IdEdit, f => f.Random.Bool(0.3f) ? f.Random.Guid() : null)
        .RuleFor(s => s.IdCancel, f => f.Random.Bool(0.1f) ? f.Random.Guid() : null)
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.ProductSales, f => new List<ProductSale>());

    /// <summary>
    /// Gera uma entidade Sale válida com dados aleatórios.
    /// A venda gerada terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Uma entidade Sale válida com dados gerados aleatoriamente.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Gera uma data de venda válida.
    /// A data gerada será:
    /// - Entre 2020 e 2030
    /// - Data realista para uma venda
    /// - Não no futuro
    /// </summary>
    /// <returns>Uma data de venda válida.</returns>
    public static DateTime GenerateValidSaleDate()
    {
        return new Faker().Date.Between(DateTime.Now.AddYears(-3), DateTime.Now);
    }

    /// <summary>
    /// Gera um valor total válido para venda.
    /// O valor gerado será:
    /// - Entre 0.01 e 100000.00
    /// - Com no máximo 2 casas decimais
    /// - Valor positivo
    /// </summary>
    /// <returns>Um valor total válido para venda.</returns>
    public static decimal GenerateValidTotal()
    {
        return new Faker().Random.Decimal(0.01m, 100000.00m);
    }

    /// <summary>
    /// Gera um desconto válido para venda.
    /// O desconto gerado será:
    /// - Entre 0.00 e 50.00
    /// - Com no máximo 2 casas decimais
    /// - Valor positivo ou zero
    /// </summary>
    /// <returns>Um desconto válido para venda.</returns>
    public static decimal GenerateValidDiscount()
    {
        return new Faker().Random.Decimal(0.00m, 50.00m);
    }

    /// <summary>
    /// Gera um ID de cliente válido.
    /// O ID gerado será:
    /// - Um GUID válido
    /// - Não vazio
    /// </summary>
    /// <returns>Um ID de cliente válido.</returns>
    public static Guid GenerateValidCustomerId()
    {
        return new Faker().Random.Guid();
    }

    /// <summary>
    /// Gera uma data de venda inválida para testar cenários negativos.
    /// A data gerada será:
    /// - No futuro
    /// - Data muito antiga
    /// - Data inválida
    /// Isso é útil para testar casos de erro de validação de data.
    /// </summary>
    /// <returns>Uma data de venda inválida.</returns>
    public static DateTime GenerateInvalidSaleDate()
    {
        var faker = new Faker();
        return faker.PickRandom(
            DateTime.Now.AddDays(1), // Futuro
            DateTime.Now.AddYears(-100), // Muito antiga
            DateTime.MinValue // Data inválida
        );
    }

    /// <summary>
    /// Gera um valor total inválido para venda para testar cenários negativos.
    /// O valor gerado será:
    /// - Negativo
    /// - Zero
    /// - Valor muito alto
    /// Isso é útil para testar casos de erro de validação de valor.
    /// </summary>
    /// <returns>Um valor total inválido para venda.</returns>
    public static decimal GenerateInvalidTotal()
    {
        var faker = new Faker();
        return faker.PickRandom(-100.00m, 0.00m, 1000000.00m);
    }

    /// <summary>
    /// Gera um desconto inválido para venda para testar cenários negativos.
    /// O desconto gerado será:
    /// - Negativo
    /// - Maior que 100%
    /// - Valor muito alto
    /// Isso é útil para testar casos de erro de validação de desconto.
    /// </summary>
    /// <returns>Um desconto inválido para venda.</returns>
    public static decimal GenerateInvalidDiscount()
    {
        var faker = new Faker();
        return faker.PickRandom(-10.00m, 101.00m, 1000.00m);
    }

    /// <summary>
    /// Gera uma venda com dados mínimos válidos.
    /// A venda gerada terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Uma venda com dados mínimos válidos.</returns>
    public static Sale GenerateMinimalValidSale()
    {
        return new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = GenerateValidSaleDate(),
            Total = GenerateValidTotal(),
            Discount = GenerateValidDiscount(),
            Cancel = false,
            DtCreate = DateTime.Now,
            IdCustomer = GenerateValidCustomerId(),
            IdCreate = GenerateValidCustomerId(),
            ProductSales = new List<ProductSale>()
        };
    }

    /// <summary>
    /// Gera uma venda cancelada válida.
    /// A venda gerada terá:
    /// - Status de cancelamento como true
    /// - Data de cancelamento preenchida
    /// - ID de cancelamento preenchido
    /// - Outros dados válidos
    /// </summary>
    /// <returns>Uma venda cancelada válida.</returns>
    public static Sale GenerateCancelledSale()
    {
        var sale = GenerateValidSale();
        sale.Cancel = true;
        sale.DtCancel = DateTime.Now;
        sale.IdCancel = Guid.NewGuid();
        return sale;
    }

    /// <summary>
    /// Gera uma venda com dados inválidos para testar cenários negativos.
    /// A venda gerada terá:
    /// - Data de venda inválida (futura ou muito antiga)
    /// - Valor total inválido (negativo, zero ou muito alto)
    /// - Desconto inválido (negativo ou maior que 100%)
    /// - IDs inválidos (vazios)
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Uma venda com dados inválidos.</returns>
    public static Sale GenerateInvalidSale()
    {
        return new Sale
        {
            Id = Guid.Empty, // Inválido: ID vazio
            DtSale = GenerateInvalidSaleDate(), // Inválido: data inválida
            Total = GenerateInvalidTotal(), // Inválido: valor inválido
            Discount = GenerateInvalidDiscount(), // Inválido: desconto inválido
            Cancel = true, // Inválido: cancelada sem dados de cancelamento
            DtCreate = DateTime.MinValue, // Inválido: data inválida
            DtEdit = DateTime.MinValue, // Inválido: data inválida
            DtCancel = null, // Inválido: cancelada mas sem data de cancelamento
            IdCustomer = Guid.Empty, // Inválido: ID vazio
            IdCreate = Guid.Empty, // Inválido: ID vazio
            IdEdit = Guid.Empty, // Inválido: ID vazio
            IdCancel = null, // Inválido: cancelada mas sem ID de cancelamento
            ProductSales = null! // Inválido: lista nula
        };
    }
} 