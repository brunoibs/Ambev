using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contém testes unitários para a classe de entidade Sale.
/// Os testes cobrem cenários de criação, propriedades e validações básicas.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Testa que uma venda pode ser criada com dados válidos.
    /// </summary>
    [Fact(DisplayName = "Sale deve ser criada com dados válidos")]
    public void Given_ValidSaleData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, sale.Id);
        Assert.True(sale.DtSale <= DateTime.Now);
        Assert.True(sale.Total > 0);
        Assert.True(sale.Total <= 100000.00m);
        Assert.True(sale.Discount >= 0);
        Assert.True(sale.Discount <= 50.00m);
        Assert.True(sale.DtCreate <= DateTime.Now);
        Assert.NotEqual(Guid.Empty, sale.IdCustomer);
        Assert.NotEqual(Guid.Empty, sale.IdCreate);
        Assert.NotNull(sale.ProductSales);
    }

    /// <summary>
    /// Testa que uma venda pode ser criada com dados mínimos válidos.
    /// </summary>
    [Fact(DisplayName = "Sale deve ser criada com dados mínimos válidos")]
    public void Given_MinimalValidSaleData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var sale = SaleTestData.GenerateMinimalValidSale();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, sale.Id);
        Assert.True(sale.DtSale <= DateTime.Now);
        Assert.True(sale.Total > 0);
        Assert.True(sale.Discount >= 0);
        Assert.False(sale.Cancel);
        Assert.True(sale.DtCreate <= DateTime.Now);
        Assert.NotEqual(Guid.Empty, sale.IdCustomer);
        Assert.NotEqual(Guid.Empty, sale.IdCreate);
        Assert.NotNull(sale.ProductSales);
    }

    /// <summary>
    /// Testa que uma venda com dados inválidos tem propriedades inválidas.
    /// </summary>
    [Fact(DisplayName = "Sale com dados inválidos deve ter propriedades inválidas")]
    public void Given_InvalidSaleData_When_Created_Then_ShouldHaveInvalidProperties()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvalidSale();

        // Act & Assert
        Assert.Equal(Guid.Empty, sale.Id);
        // Verifica se pelo menos uma das propriedades é inválida
        var hasInvalidSaleDate = sale.DtSale > DateTime.Now || sale.DtSale < DateTime.Now.AddYears(-100);
        var hasInvalidTotal = sale.Total <= 0 || sale.Total > 1000000.00m;
        var hasInvalidDiscount = sale.Discount < 0 || sale.Discount > 100.00m;
        var hasInvalidIds = sale.IdCustomer == Guid.Empty || sale.IdCreate == Guid.Empty;
        var hasInvalidProductSales = sale.ProductSales == null;
        
        Assert.True(hasInvalidSaleDate || hasInvalidTotal || hasInvalidDiscount || hasInvalidIds || hasInvalidProductSales, 
            "A venda deve ter pelo menos uma propriedade inválida");
    }

    /// <summary>
    /// Testa que uma venda cancelada tem propriedades corretas.
    /// </summary>
    [Fact(DisplayName = "Sale cancelada deve ter propriedades corretas")]
    public void Given_CancelledSale_When_Created_Then_ShouldHaveCancelledProperties()
    {
        // Arrange
        var sale = SaleTestData.GenerateCancelledSale();

        // Act & Assert
        Assert.True(sale.Cancel);
        Assert.NotNull(sale.DtCancel);
        Assert.NotEqual(Guid.Empty, sale.IdCancel);
        Assert.True(sale.DtCancel <= DateTime.Now);
    }

    /// <summary>
    /// Testa que a data de venda pode ser alterada.
    /// </summary>
    [Fact(DisplayName = "Data de venda deve poder ser alterada")]
    public void Given_ValidSale_When_SaleDateChanged_Then_ShouldUpdateSaleDate()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newSaleDate = SaleTestData.GenerateValidSaleDate();

        // Act
        sale.DtSale = newSaleDate;

        // Assert
        Assert.Equal(newSaleDate, sale.DtSale);
    }

    /// <summary>
    /// Testa que o valor total pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Valor total deve poder ser alterado")]
    public void Given_ValidSale_When_TotalChanged_Then_ShouldUpdateTotal()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newTotal = SaleTestData.GenerateValidTotal();

        // Act
        sale.Total = newTotal;

        // Assert
        Assert.Equal(newTotal, sale.Total);
    }

    /// <summary>
    /// Testa que o desconto pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Desconto deve poder ser alterado")]
    public void Given_ValidSale_When_DiscountChanged_Then_ShouldUpdateDiscount()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newDiscount = SaleTestData.GenerateValidDiscount();

        // Act
        sale.Discount = newDiscount;

        // Assert
        Assert.Equal(newDiscount, sale.Discount);
    }

    /// <summary>
    /// Testa que o status de cancelamento pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Status de cancelamento deve poder ser alterado")]
    public void Given_ValidSale_When_CancelStatusChanged_Then_ShouldUpdateCancelStatus()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.Cancel = true;

        // Assert
        Assert.True(sale.Cancel);
    }

    /// <summary>
    /// Testa que uma venda pode ser criada com construtor padrão.
    /// </summary>
    [Fact(DisplayName = "Sale deve ser criada com construtor padrão")]
    public void Given_DefaultConstructor_When_Created_Then_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var sale = new Sale();

        // Assert
        Assert.NotNull(sale);
        Assert.Equal(DateTime.MinValue, sale.DtSale);
        Assert.Equal(0, sale.Total);
        Assert.Equal(0, sale.Discount);
        Assert.False(sale.Cancel);
        Assert.Equal(DateTime.MinValue, sale.DtCreate);
        Assert.Null(sale.DtEdit);
        Assert.Null(sale.DtCancel);
        Assert.Equal(Guid.Empty, sale.IdCustomer);
        Assert.Equal(Guid.Empty, sale.IdCreate);
        Assert.Null(sale.IdEdit);
        Assert.Null(sale.IdCancel);
        Assert.NotNull(sale.ProductSales);
        Assert.Empty(sale.ProductSales);
    }

    /// <summary>
    /// Testa que o ID da venda é único.
    /// </summary>
    [Fact(DisplayName = "IDs de Sale devem ser únicos")]
    public void Given_MultipleSales_When_Created_Then_ShouldHaveUniqueIds()
    {
        // Arrange & Act
        var sale1 = SaleTestData.GenerateValidSale();
        var sale2 = SaleTestData.GenerateValidSale();
        var sale3 = SaleTestData.GenerateValidSale();

        // Assert
        Assert.NotEqual(sale1.Id, sale2.Id);
        Assert.NotEqual(sale2.Id, sale3.Id);
        Assert.NotEqual(sale1.Id, sale3.Id);
    }

    /// <summary>
    /// Testa que a data de edição pode ser definida.
    /// </summary>
    [Fact(DisplayName = "Data de edição deve poder ser definida")]
    public void Given_ValidSale_When_EditDateSet_Then_ShouldUpdateEditDate()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var editDate = DateTime.Now;

        // Act
        sale.DtEdit = editDate;

        // Assert
        Assert.Equal(editDate, sale.DtEdit);
    }

    /// <summary>
    /// Testa que a data de cancelamento pode ser definida.
    /// </summary>
    [Fact(DisplayName = "Data de cancelamento deve poder ser definida")]
    public void Given_ValidSale_When_CancelDateSet_Then_ShouldUpdateCancelDate()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var cancelDate = DateTime.Now;

        // Act
        sale.DtCancel = cancelDate;

        // Assert
        Assert.Equal(cancelDate, sale.DtCancel);
    }

    /// <summary>
    /// Testa que o ID do cliente pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "ID do cliente deve poder ser alterado")]
    public void Given_ValidSale_When_CustomerIdChanged_Then_ShouldUpdateCustomerId()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newCustomerId = SaleTestData.GenerateValidCustomerId();

        // Act
        sale.IdCustomer = newCustomerId;

        // Assert
        Assert.Equal(newCustomerId, sale.IdCustomer);
    }

    /// <summary>
    /// Testa que o ID de criação pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "ID de criação deve poder ser alterado")]
    public void Given_ValidSale_When_CreateIdChanged_Then_ShouldUpdateCreateId()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newCreateId = SaleTestData.GenerateValidCustomerId();

        // Act
        sale.IdCreate = newCreateId;

        // Assert
        Assert.Equal(newCreateId, sale.IdCreate);
    }

    /// <summary>
    /// Testa que o ID de edição pode ser definido.
    /// </summary>
    [Fact(DisplayName = "ID de edição deve poder ser definido")]
    public void Given_ValidSale_When_EditIdSet_Then_ShouldUpdateEditId()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var editId = Guid.NewGuid();

        // Act
        sale.IdEdit = editId;

        // Assert
        Assert.Equal(editId, sale.IdEdit);
    }

    /// <summary>
    /// Testa que o ID de cancelamento pode ser definido.
    /// </summary>
    [Fact(DisplayName = "ID de cancelamento deve poder ser definido")]
    public void Given_ValidSale_When_CancelIdSet_Then_ShouldUpdateCancelId()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var cancelId = Guid.NewGuid();

        // Act
        sale.IdCancel = cancelId;

        // Assert
        Assert.Equal(cancelId, sale.IdCancel);
    }

    /// <summary>
    /// Testa que a lista de produtos da venda pode ser alterada.
    /// </summary>
    [Fact(DisplayName = "Lista de produtos da venda deve poder ser alterada")]
    public void Given_ValidSale_When_ProductSalesChanged_Then_ShouldUpdateProductSales()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var newProductSales = new List<ProductSale>();

        // Act
        sale.ProductSales = newProductSales;

        // Assert
        Assert.Equal(newProductSales, sale.ProductSales);
    }

    /// <summary>
    /// Testa que uma venda pode ter valor total zero.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter valor total zero")]
    public void Given_SaleWithZeroTotal_When_Created_Then_ShouldHandleZeroTotal()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Total = 0;

        // Assert
        Assert.Equal(0, sale.Total);
    }

    /// <summary>
    /// Testa que uma venda pode ter valor total negativo.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter valor total negativo")]
    public void Given_SaleWithNegativeTotal_When_Created_Then_ShouldHandleNegativeTotal()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Total = -100.00m;

        // Assert
        Assert.Equal(-100.00m, sale.Total);
    }

    /// <summary>
    /// Testa que uma venda pode ter desconto zero.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter desconto zero")]
    public void Given_SaleWithZeroDiscount_When_Created_Then_ShouldHandleZeroDiscount()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Discount = 0;

        // Assert
        Assert.Equal(0, sale.Discount);
    }

    /// <summary>
    /// Testa que uma venda pode ter desconto negativo.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter desconto negativo")]
    public void Given_SaleWithNegativeDiscount_When_Created_Then_ShouldHandleNegativeDiscount()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Discount = -10.00m;

        // Assert
        Assert.Equal(-10.00m, sale.Discount);
    }

    /// <summary>
    /// Testa que uma venda pode ter desconto maior que 100%.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter desconto maior que 100%")]
    public void Given_SaleWithHighDiscount_When_Created_Then_ShouldHandleHighDiscount()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.Discount = 150.00m;

        // Assert
        Assert.Equal(150.00m, sale.Discount);
    }

    /// <summary>
    /// Testa que uma venda pode ter data de venda no futuro.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter data de venda no futuro")]
    public void Given_SaleWithFutureDate_When_Created_Then_ShouldHandleFutureDate()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.DtSale = DateTime.Now.AddDays(1);

        // Assert
        Assert.True(sale.DtSale > DateTime.Now);
    }

    /// <summary>
    /// Testa que uma venda pode ter data de venda muito antiga.
    /// </summary>
    [Fact(DisplayName = "Sale pode ter data de venda muito antiga")]
    public void Given_SaleWithOldDate_When_Created_Then_ShouldHandleOldDate()
    {
        // Arrange
        var sale = new Sale();

        // Act
        sale.DtSale = DateTime.Now.AddYears(-100);

        // Assert
        Assert.True(sale.DtSale < DateTime.Now.AddYears(-50));
    }
} 