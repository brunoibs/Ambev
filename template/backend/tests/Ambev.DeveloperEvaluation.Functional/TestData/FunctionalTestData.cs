using Bogus;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Functional.TestData;

/// <summary>
/// Dados de teste para testes funcionais
/// </summary>
public static class FunctionalTestData
{
    private static readonly Faker _faker = new Faker("pt_BR");

    /// <summary>
    /// Gera dados válidos para criação de venda
    /// </summary>
    public static CreateSaleRequest GenerateValidCreateSaleRequest()
    {
        return new CreateSaleRequest
        {
            DtSale = DateTime.Now,
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
            ProductSales = GenerateValidProductSales()
        };
    }

    /// <summary>
    /// Gera dados válidos para criação de venda com desconto
    /// </summary>
    public static CreateSaleRequest GenerateValidCreateSaleRequestWithDiscount()
    {
        var request = GenerateValidCreateSaleRequest();
        
        // Configura produtos com quantidade para aplicar desconto
        request.ProductSales = new List<ProductSaleRequest>
        {
            new ProductSaleRequest
            {
                IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                Amount = 5, // Quantidade que aplica 10% de desconto
                Price = 100.00m,
                Total = 450.00m // 500 - 10% de desconto
            }
        };

        return request;
    }

    /// <summary>
    /// Gera dados válidos para criação de venda com desconto máximo
    /// </summary>
    public static CreateSaleRequest GenerateValidCreateSaleRequestWithMaxDiscount()
    {
        var request = GenerateValidCreateSaleRequest();
        
        // Configura produtos com quantidade para aplicar desconto máximo
        request.ProductSales = new List<ProductSaleRequest>
        {
            new ProductSaleRequest
            {
                IdProduct = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"),
                Amount = 15, // Quantidade que aplica 20% de desconto
                Price = 100.00m,
                Total = 1200.00m // 1500 - 20% de desconto
            }
        };

        return request;
    }

    /// <summary>
    /// Gera dados inválidos para criação de venda (quantidade máxima excedida)
    /// </summary>
    public static CreateSaleRequest GenerateInvalidCreateSaleRequestExceedingMaxItems()
    {
        var request = GenerateValidCreateSaleRequest();
        
        request.ProductSales = new List<ProductSaleRequest>
        {
            new ProductSaleRequest
            {
                IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                Amount = 25, // Quantidade que excede o limite de 20
                Price = 100.00m,
                Total = 2500.00m
            }
        };

        return request;
    }

    /// <summary>
    /// Gera dados inválidos para criação de venda (quantidade insuficiente para desconto)
    /// </summary>
    public static CreateSaleRequest GenerateInvalidCreateSaleRequestInsufficientItemsForDiscount()
    {
        var request = GenerateValidCreateSaleRequest();
        
        request.ProductSales = new List<ProductSaleRequest>
        {
            new ProductSaleRequest
            {
                IdProduct = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"),
                Amount = 2, // Quantidade insuficiente para desconto
                Price = 100.00m,
                Total = 200.00m
            }
        };

        return request;
    }

    /// <summary>
    /// Gera dados válidos para autenticação de usuário
    /// </summary>
    public static AuthenticateUserRequest GenerateValidAuthenticateUserRequest()
    {
        return new Faker<AuthenticateUserRequest>("pt_BR")
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password())
            .Generate();
    }

    /// <summary>
    /// Gera dados válidos para criação de usuário
    /// </summary>
    public static CreateUserRequest GenerateValidCreateUserRequest()
    {
        return new CreateUserRequest
        {
            Username = "TestUser123",
            Email = "test@example.com",
            Password = "Test@123!", // Senha com caractere especial
            Phone = "+5511999999999", // Telefone no formato internacional
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };
    }

    /// <summary>
    /// Gera dados inválidos para criação de usuário (email inválido)
    /// </summary>
    public static CreateUserRequest GenerateInvalidCreateUserRequestWithInvalidEmail()
    {
        var request = GenerateValidCreateUserRequest();
        request.Email = "email-invalido";
        return request;
    }

    /// <summary>
    /// Gera dados inválidos para criação de usuário (senha fraca)
    /// </summary>
    public static CreateUserRequest GenerateInvalidCreateUserRequestWithWeakPassword()
    {
        var request = GenerateValidCreateUserRequest();
        request.Password = "123"; // Senha muito curta
        return request;
    }

    /// <summary>
    /// Gera lista válida de produtos de venda
    /// </summary>
    private static List<ProductSaleRequest> GenerateValidProductSales()
    {
        var validProductIds = new[]
        {
            Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"), // Guarana - R$ 7,00
            Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"), // Agua - R$ 3,00
            Guid.Parse("27a922a0-4c35-4344-95ae-ee622ee87c80"), // Skol - R$ 10,00
            Guid.Parse("f7627e62-aeaa-4224-8018-f968b47781d0")  // Suco - R$ 5,00
        };

        return new List<ProductSaleRequest>
        {
            new ProductSaleRequest
            {
                IdProduct = validProductIds[0], // Guarana
                Amount = 10,
                Price = 7.00m,
                Total = 70.00m
            },
            new ProductSaleRequest
            {
                IdProduct = validProductIds[1], // Agua
                Amount = 10,
                Price = 3.00m,
                Total = 30.00m
            }
        };
    }
} 