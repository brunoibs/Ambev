using Ambev.DeveloperEvaluation.Application.Sale.Create;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using ProductSaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.ProductSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Fornece métodos para gerar dados de teste usando a biblioteca Bogus.
/// Esta classe centraliza toda a geração de dados de teste para garantir consistência
/// entre casos de teste e fornecer cenários de dados válidos e inválidos.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configura o Faker para gerar comandos CreateSaleCommand válidos.
    /// Os comandos gerados terão:
    /// - Data de venda válida
    /// - Cliente válido
    /// - ProductSales válidos
    /// - Dados consistentes e realistas
    /// </summary>
    private static readonly Faker<CreateSaleCommand> CreateSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.DtSale, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.Total, f => f.Random.Decimal(10.00m, 1000.00m))
        .RuleFor(c => c.Discount, f => f.Random.Decimal(0.00m, 100.00m))
        .RuleFor(c => c.Cancel, f => f.Random.Bool(0.1f))
        .RuleFor(c => c.DtCreate, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.DtEdit, f => f.Random.Bool(0.3f) ? f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now) : null)
        .RuleFor(c => c.DtCancel, f => f.Random.Bool(0.1f) ? f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now) : null)
        .RuleFor(c => c.IdCustomer, f => f.Random.Guid())
        .RuleFor(c => c.IdCreate, f => f.Random.Guid())
        .RuleFor(c => c.IdEdit, f => f.Random.Bool(0.3f) ? f.Random.Guid() : null)
        .RuleFor(c => c.IdCancel, f => f.Random.Bool(0.1f) ? f.Random.Guid() : null)
        .RuleFor(c => c.ProductSales, f => GenerateValidProductSales());

    /// <summary>
    /// Gera uma lista de ProductSales válidos.
    /// </summary>
    /// <returns>Uma lista de ProductSales válidos.</returns>
    private static List<ProductSaleEntity> GenerateValidProductSales()
    {
        var faker = new Faker();
        var productSales = new List<ProductSaleEntity>();
        
        for (int i = 0; i < faker.Random.Int(1, 5); i++)
        {
            var price = faker.Random.Decimal(5.00m, 100.00m);
            var amount = faker.Random.Int(1, 10);
            var total = price * amount;
            
            productSales.Add(new ProductSaleEntity
            {
                IdProduct = faker.Random.Guid(),
                Amount = amount,
                Price = price,
                Total = total
            });
        }
        
        return productSales;
    }

    /// <summary>
    /// Gera um comando CreateSaleCommand válido com dados aleatórios.
    /// O comando gerado terá todas as propriedades preenchidas com valores válidos
    /// que atendem aos requisitos do sistema.
    /// </summary>
    /// <returns>Um comando CreateSaleCommand válido com dados gerados aleatoriamente.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleCommandFaker.Generate();
    }

    /// <summary>
    /// Gera um comando CreateSaleCommand com dados inválidos para testar cenários negativos.
    /// O comando gerado terá:
    /// - Data de venda inválida
    /// - Cliente inválido
    /// - ProductSales inválidos
    /// - Dados que não atendem aos requisitos do sistema
    /// </summary>
    /// <returns>Um comando CreateSaleCommand com dados inválidos.</returns>
    public static CreateSaleCommand GenerateInvalidCommand()
    {
        return new CreateSaleCommand
        {
            DtSale = DateTime.MinValue, // Inválido: data muito antiga
            Total = -100.00m, // Inválido: total negativo
            Discount = -50.00m, // Inválido: desconto negativo
            Cancel = false,
            DtCreate = DateTime.MinValue,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.Empty, // Inválido: ID vazio
            IdCreate = Guid.Empty, // Inválido: ID vazio
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSaleEntity>() // Inválido: lista vazia
        };
    }

    /// <summary>
    /// Gera um comando CreateSaleCommand com lista de ProductSales vazia para testar cenários negativos.
    /// </summary>
    /// <returns>Um comando CreateSaleCommand com lista de ProductSales vazia.</returns>
    public static CreateSaleCommand GenerateCommandWithEmptyProductSales()
    {
        var command = CreateSaleCommandFaker.Generate();
        command.ProductSales = new List<ProductSaleEntity>();
        return command;
    }

    /// <summary>
    /// Gera um comando CreateSaleCommand com ProductSales inválidos para testar cenários negativos.
    /// </summary>
    /// <returns>Um comando CreateSaleCommand com ProductSales inválidos.</returns>
    public static CreateSaleCommand GenerateCommandWithInvalidProductSales()
    {
        var command = CreateSaleCommandFaker.Generate();
        command.ProductSales = new List<ProductSaleEntity>
        {
            new ProductSaleEntity
            {
                IdProduct = Guid.Empty, // Inválido: ID vazio
                Amount = -1, // Inválido: quantidade negativa
                Price = -10.00m, // Inválido: preço negativo
                Total = -100.00m // Inválido: total negativo
            }
        };
        return command;
    }

    /// <summary>
    /// Gera um comando CreateSaleCommand com dados mínimos válidos.
    /// O comando gerado terá apenas os dados essenciais preenchidos.
    /// </summary>
    /// <returns>Um comando CreateSaleCommand com dados mínimos válidos.</returns>
    public static CreateSaleCommand GenerateMinimalValidCommand()
    {
        return new CreateSaleCommand
        {
            DtSale = DateTime.Now,
            Total = 50.00m,
            Discount = 5.00m,
            Cancel = false,
            DtCreate = DateTime.Now,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.NewGuid(),
            IdCreate = Guid.NewGuid(),
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSaleEntity>
            {
                new ProductSaleEntity
                {
                    IdProduct = Guid.NewGuid(),
                    Amount = 1,
                    Price = 50.00m,
                    Total = 50.00m
                }
            }
        };
    }

    /// <summary>
    /// Gera um ProductSale válido.
    /// </summary>
    /// <returns>Um ProductSale válido.</returns>
    public static ProductSaleEntity GenerateValidProductSale()
    {
        var faker = new Faker();
        var price = faker.Random.Decimal(10.00m, 100.00m);
        var amount = faker.Random.Int(1, 5);
        var total = price * amount;
        
        return new ProductSaleEntity
        {
            IdProduct = faker.Random.Guid(),
            Amount = amount,
            Price = price,
            Total = total
        };
    }

    /// <summary>
    /// Gera um ProductSale inválido.
    /// </summary>
    /// <returns>Um ProductSale inválido.</returns>
    public static ProductSaleEntity GenerateInvalidProductSale()
    {
        return new ProductSaleEntity
        {
            IdProduct = Guid.Empty, // Inválido: ID vazio
            Amount = -1, // Inválido: quantidade negativa
            Price = -10.00m, // Inválido: preço negativo
            Total = -100.00m // Inválido: total negativo
        };
    }
} 