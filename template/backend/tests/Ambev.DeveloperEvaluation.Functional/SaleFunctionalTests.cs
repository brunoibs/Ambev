using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.Functional.TestData;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

/// <summary>
/// Testes funcionais para o módulo de vendas baseados na estrutura da tabela Sale
/// </summary>
public class SaleFunctionalTests : FunctionalTestBase
{
    public SaleFunctionalTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact(DisplayName = "Given valid sale data with discount When creating sale Then returns success with correct data")]
    public async Task CreateSale_WithValidDataAndDiscount_ReturnsSuccess()
    {
        // Arrange - Baseado no script da tabela Sale
        var request = new CreateSaleRequest
        {
            DtSale = DateTime.Parse("2024-01-15T14:30:00Z"),
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
            ProductSales = new List<ProductSaleRequest>
            {
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                    Amount = 10,
                    Price = 7.00m,
                    Total = 70.00m
                },
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"),
                    Amount = 10,
                    Price = 3.00m,
                    Total = 30.00m
                }
            }
        };

        // Act
        var response = await PostAsync("/api/sale", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseContent = await DeserializeResponseAsync<ApiResponseWithData<CreateSaleResponse>>(response);
        responseContent.Should().NotBeNull();
        responseContent!.Success.Should().BeTrue();
        responseContent.Message.Should().Be("Venda criada com sucesso");
        responseContent.Data.Should().NotBeNull();
        responseContent.Data!.Id.Should().NotBeEmpty();
        
        // Cálculo correto: Total original 100.00m, desconto 20% = 80.00m
        responseContent.Data.Total.Should().Be(80.00m); // 100 - 20% de desconto
        responseContent.Data.Discount.Should().Be(20.00m); // 20% de desconto aplicado
        responseContent.Data.IdCustomer.Should().Be(request.IdCustomer);
        responseContent.Data.IdCreate.Should().Be(request.IdCreate);
    }

    [Fact(DisplayName = "Given sale with specific ID When creating sale Then returns sale with correct ID")]
    public async Task CreateSale_WithSpecificId_ReturnsCorrectId()
    {
        // Arrange - Baseado no script da tabela Sale com ID específico
        var expectedId = Guid.Parse("5dbc3b9e-eb36-405f-ab04-f18b0ce2c638");
        var request = new CreateSaleRequest
        {
            DtSale = DateTime.Parse("2024-01-15T14:30:00Z"),
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
            ProductSales = new List<ProductSaleRequest>
            {
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                    Amount = 10,
                    Price = 7.00m,
                    Total = 70.00m
                },
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"),
                    Amount = 10,
                    Price = 3.00m,
                    Total = 30.00m
                }
            }
        };

        // Act
        var response = await PostAsync("/api/sale", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseContent = await DeserializeResponseAsync<ApiResponseWithData<CreateSaleResponse>>(response);
        responseContent.Should().NotBeNull();
        responseContent!.Success.Should().BeTrue();
        responseContent.Data.Should().NotBeNull();
        responseContent.Data!.Id.Should().NotBeEmpty();
        
        // Cálculo correto: Total original 100.00m, desconto 20% = 80.00m
        responseContent.Data.Total.Should().Be(80.00m); // 100 - 20% de desconto
        responseContent.Data.Discount.Should().Be(20.00m); // 20% de desconto aplicado
    }

    [Fact(DisplayName = "Given sale with customer and branch When creating sale Then validates customer and branch")]
    public async Task CreateSale_WithCustomerAndBranch_ValidatesCorrectly()
    {
        // Arrange - Testando validação de cliente e filial
        var request = new CreateSaleRequest
        {
            DtSale = DateTime.Now,
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"), // Cliente válido
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"), // Filial válida
            ProductSales = new List<ProductSaleRequest>
            {
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                    Amount = 5,
                    Price = 7.00m,
                    Total = 35.00m
                }
            }
        };

        // Act
        var response = await PostAsync("/api/sale", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseContent = await DeserializeResponseAsync<ApiResponseWithData<CreateSaleResponse>>(response);
        responseContent.Should().NotBeNull();
        responseContent!.Success.Should().BeTrue();
        responseContent.Data.Should().NotBeNull();
        responseContent.Data!.IdCustomer.Should().Be(request.IdCustomer);
        responseContent.Data.IdCreate.Should().Be(request.IdCreate);
    }

    [Fact(DisplayName = "Given sale with multiple products When creating sale Then calculates total correctly")]
    public async Task CreateSale_WithMultipleProducts_CalculatesTotalCorrectly()
    {
        // Arrange - Testando cálculo de total com múltiplos produtos
        var request = new CreateSaleRequest
        {
            DtSale = DateTime.Now,
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
            ProductSales = new List<ProductSaleRequest>
            {
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"), // Guarana
                    Amount = 2,
                    Price = 7.00m,
                    Total = 14.00m
                },
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9"), // Agua
                    Amount = 3,
                    Price = 3.00m,
                    Total = 9.00m
                },
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("27a922a0-4c35-4344-95ae-ee622ee87c80"), // Skol
                    Amount = 1,
                    Price = 10.00m,
                    Total = 10.00m
                }
            }
        };

        // Act
        var response = await PostAsync("/api/sale", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseContent = await DeserializeResponseAsync<ApiResponseWithData<CreateSaleResponse>>(response);
        responseContent.Should().NotBeNull();
        responseContent!.Success.Should().BeTrue();
        responseContent.Data.Should().NotBeNull();
        responseContent.Data!.Total.Should().Be(33.00m); // 14 + 9 + 10
        responseContent.Data.IdCustomer.Should().Be(request.IdCustomer);
        responseContent.Data.IdCreate.Should().Be(request.IdCreate);
    }

    [Fact(DisplayName = "Given sale with date and time When creating sale Then stores date correctly")]
    public async Task CreateSale_WithDateTime_StoresDateCorrectly()
    {
        // Arrange - Testando armazenamento de data/hora
        var saleDate = DateTime.Parse("2024-01-15T14:30:00Z");
        var request = new CreateSaleRequest
        {
            DtSale = saleDate,
            IdCustomer = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdCreate = Guid.Parse("7784319a-5eaf-408f-b8e5-40d6506ffd54"),
            IdBranch = Guid.Parse("51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a"),
            ProductSales = new List<ProductSaleRequest>
            {
                new ProductSaleRequest
                {
                    IdProduct = Guid.Parse("039e444c-028e-4d7e-b564-b30a2ca11167"),
                    Amount = 1,
                    Price = 7.00m,
                    Total = 7.00m
                }
            }
        };

        // Act
        var response = await PostAsync("/api/sale", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseContent = await DeserializeResponseAsync<ApiResponseWithData<CreateSaleResponse>>(response);
        responseContent.Should().NotBeNull();
        responseContent!.Success.Should().BeTrue();
        responseContent.Data.Should().NotBeNull();
        responseContent.Data!.DtSale.Should().Be(saleDate);
    }
} 