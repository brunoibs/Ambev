using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contém testes unitários para a classe de entidade Product.
/// Os testes cobrem cenários de criação, propriedades e validações básicas.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Testa que um produto pode ser criado com dados válidos.
    /// </summary>
    [Fact(DisplayName = "Product deve ser criado com dados válidos")]
    public void Given_ValidProductData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.NotEmpty(product.Name);
        Assert.True(product.Name.Length >= 3);
        Assert.True(product.Name.Length <= 100);
        Assert.True(product.Price > 0);
        Assert.True(product.Price <= 10000.00m);
        Assert.True(product.Amount >= 0);
        Assert.True(product.Amount <= 10000);
    }

    /// <summary>
    /// Testa que um produto pode ser criado com dados mínimos válidos.
    /// </summary>
    [Fact(DisplayName = "Product deve ser criado com dados mínimos válidos")]
    public void Given_MinimalValidProductData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var product = ProductTestData.GenerateMinimalValidProduct();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.NotEmpty(product.Name);
        Assert.True(product.Name.Length >= 3);
        Assert.True(product.Price > 0);
        Assert.True(product.Amount >= 0);
    }

    /// <summary>
    /// Testa que um produto com dados inválidos tem propriedades inválidas.
    /// </summary>
    [Fact(DisplayName = "Product com dados inválidos deve ter propriedades inválidas")]
    public void Given_InvalidProductData_When_Created_Then_ShouldHaveInvalidProperties()
    {
        // Arrange
        var product = ProductTestData.GenerateInvalidProduct();

        // Act & Assert
        Assert.Equal(Guid.Empty, product.Id);
        // Verifica se pelo menos uma das propriedades é inválida
        var hasInvalidName = string.IsNullOrEmpty(product.Name) || product.Name.Length < 3;
        var hasInvalidPrice = product.Price <= 0 || product.Price > 100000.00m;
        var hasInvalidAmount = product.Amount < 0 || product.Amount > 1000000;
        
        Assert.True(hasInvalidName || hasInvalidPrice || hasInvalidAmount, 
            "O produto deve ter pelo menos uma propriedade inválida");
    }

    /// <summary>
    /// Testa que o nome do produto pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Nome do Product deve poder ser alterado")]
    public void Given_ValidProduct_When_NameChanged_Then_ShouldUpdateName()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var newName = ProductTestData.GenerateValidProductName();

        // Act
        product.Name = newName;

        // Assert
        Assert.Equal(newName, product.Name);
    }

    /// <summary>
    /// Testa que o preço do produto pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Preço do Product deve poder ser alterado")]
    public void Given_ValidProduct_When_PriceChanged_Then_ShouldUpdatePrice()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var newPrice = ProductTestData.GenerateValidPrice();

        // Act
        product.Price = newPrice;

        // Assert
        Assert.Equal(newPrice, product.Price);
    }

    /// <summary>
    /// Testa que a quantidade do produto pode ser alterada.
    /// </summary>
    [Fact(DisplayName = "Quantidade do Product deve poder ser alterada")]
    public void Given_ValidProduct_When_AmountChanged_Then_ShouldUpdateAmount()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var newAmount = ProductTestData.GenerateValidAmount();

        // Act
        product.Amount = newAmount;

        // Assert
        Assert.Equal(newAmount, product.Amount);
    }

    /// <summary>
    /// Testa que um produto pode ser criado com construtor padrão.
    /// </summary>
    [Fact(DisplayName = "Product deve ser criado com construtor padrão")]
    public void Given_DefaultConstructor_When_Created_Then_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product);
        Assert.Equal(string.Empty, product.Name);
        Assert.Equal(0, product.Price);
        Assert.Equal(0, product.Amount);
    }

    /// <summary>
    /// Testa que o ID do produto é único.
    /// </summary>
    [Fact(DisplayName = "IDs de Product devem ser únicos")]
    public void Given_MultipleProducts_When_Created_Then_ShouldHaveUniqueIds()
    {
        // Arrange & Act
        var product1 = ProductTestData.GenerateValidProduct();
        var product2 = ProductTestData.GenerateValidProduct();
        var product3 = ProductTestData.GenerateValidProduct();

        // Assert
        Assert.NotEqual(product1.Id, product2.Id);
        Assert.NotEqual(product2.Id, product3.Id);
        Assert.NotEqual(product1.Id, product3.Id);
    }

    /// <summary>
    /// Testa que o preço do produto pode ser zero.
    /// </summary>
    [Fact(DisplayName = "Preço do Product pode ser zero")]
    public void Given_ProductWithZeroPrice_When_Created_Then_ShouldHandleZeroPrice()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 0;

        // Assert
        Assert.Equal(0, product.Price);
    }

    /// <summary>
    /// Testa que o preço do produto pode ser negativo.
    /// </summary>
    [Fact(DisplayName = "Preço do Product pode ser negativo")]
    public void Given_ProductWithNegativePrice_When_Created_Then_ShouldHandleNegativePrice()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = -10.00m;

        // Assert
        Assert.Equal(-10.00m, product.Price);
    }

    /// <summary>
    /// Testa que a quantidade do produto pode ser zero.
    /// </summary>
    [Fact(DisplayName = "Quantidade do Product pode ser zero")]
    public void Given_ProductWithZeroAmount_When_Created_Then_ShouldHandleZeroAmount()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Amount = 0;

        // Assert
        Assert.Equal(0, product.Amount);
    }

    /// <summary>
    /// Testa que a quantidade do produto pode ser negativa.
    /// </summary>
    [Fact(DisplayName = "Quantidade do Product pode ser negativa")]
    public void Given_ProductWithNegativeAmount_When_Created_Then_ShouldHandleNegativeAmount()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Amount = -5;

        // Assert
        Assert.Equal(-5, product.Amount);
    }

    /// <summary>
    /// Testa que o nome do produto pode ser nulo.
    /// </summary>
    [Fact(DisplayName = "Nome do Product pode ser nulo")]
    public void Given_ProductWithNullName_When_Created_Then_ShouldHandleNullName()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = null!;

        // Assert
        Assert.Null(product.Name);
    }

    /// <summary>
    /// Testa que o nome do produto pode ser uma string vazia.
    /// </summary>
    [Fact(DisplayName = "Nome do Product pode ser string vazia")]
    public void Given_ProductWithEmptyName_When_Created_Then_ShouldHandleEmptyName()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = string.Empty;

        // Assert
        Assert.Equal(string.Empty, product.Name);
    }

    /// <summary>
    /// Testa que um produto pode ter um nome longo.
    /// </summary>
    [Fact(DisplayName = "Product deve aceitar nome longo")]
    public void Given_ProductWithLongName_When_Created_Then_ShouldHandleLongName()
    {
        // Arrange
        var product = new Product();
        var longName = ProductTestData.GenerateLongProductName();

        // Act
        product.Name = longName;

        // Assert
        Assert.Equal(longName, product.Name);
        Assert.True(product.Name.Length > 100);
    }

    /// <summary>
    /// Testa que um produto pode ter um preço alto.
    /// </summary>
    [Fact(DisplayName = "Product deve aceitar preço alto")]
    public void Given_ProductWithHighPrice_When_Created_Then_ShouldHandleHighPrice()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 99999.99m;

        // Assert
        Assert.Equal(99999.99m, product.Price);
    }

    /// <summary>
    /// Testa que um produto pode ter uma quantidade alta.
    /// </summary>
    [Fact(DisplayName = "Product deve aceitar quantidade alta")]
    public void Given_ProductWithHighAmount_When_Created_Then_ShouldHandleHighAmount()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Amount = 9999;

        // Assert
        Assert.Equal(9999, product.Amount);
    }
} 