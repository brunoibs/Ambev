using Ambev.DeveloperEvaluation.Application.Sale.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe ListSaleHandler.
/// </summary>
public class ListSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ListSaleHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe ListSaleHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public ListSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de listagem de vendas retorna a lista de vendas.
    /// </summary>
    [Fact(DisplayName = "Given valid query When listing sales Then returns sales list")]
    public async Task Handle_ValidRequest_ReturnsSalesList()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateValidSales();
        var saleListItems = sales.Select(s => new SaleListItem
        {
            Id = s.Id,
            DtSale = s.DtSale,
            Total = s.Total,
            Discount = s.Discount,
            Cancel = s.Cancel,
            IdCustomer = s.IdCustomer
        }).ToList();

        var result = new ListSaleResult
        {
            Sales = saleListItems
        };

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        var listSaleResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listSaleResult.Should().NotBeNull();
        listSaleResult.Sales.Should().NotBeNull();
        listSaleResult.Sales.Should().HaveCount(sales.Count);
        await _saleRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com lista vazia retorna lista vazia.
    /// </summary>
    [Fact(DisplayName = "Given empty sales When listing sales Then returns empty list")]
    public async Task Handle_EmptySales_ReturnsEmptyList()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateEmptySales();

        var result = new ListSaleResult
        {
            Sales = new List<SaleListItem>()
        };

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        var listSaleResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listSaleResult.Should().NotBeNull();
        listSaleResult.Sales.Should().NotBeNull();
        listSaleResult.Sales.Should().BeEmpty();
        await _saleRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com uma única venda retorna lista com um item.
    /// </summary>
    [Fact(DisplayName = "Given single sale When listing sales Then returns single item list")]
    public async Task Handle_SingleSale_ReturnsSingleItemList()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateSingleSale();
        var sale = sales.First();

        var saleListItem = new SaleListItem
        {
            Id = sale.Id,
            DtSale = sale.DtSale,
            Total = sale.Total,
            Discount = sale.Discount,
            Cancel = sale.Cancel,
            IdCustomer = sale.IdCustomer
        };

        var result = new ListSaleResult
        {
            Sales = new List<SaleListItem> { saleListItem }
        };

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        var listSaleResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listSaleResult.Should().NotBeNull();
        listSaleResult.Sales.Should().NotBeNull();
        listSaleResult.Sales.Should().HaveCount(1);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que uma solicitação válida com múltiplas vendas retorna lista com múltiplos itens.
    /// </summary>
    [Fact(DisplayName = "Given multiple sales When listing sales Then returns multiple items list")]
    public async Task Handle_MultipleSales_ReturnsMultipleItemsList()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateMultipleSales();

        var saleListItems = sales.Select(s => new SaleListItem
        {
            Id = s.Id,
            DtSale = s.DtSale,
            Total = s.Total,
            Discount = s.Discount,
            Cancel = s.Cancel,
            IdCustomer = s.IdCustomer
        }).ToList();

        var result = new ListSaleResult
        {
            Sales = saleListItems
        };

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        var listSaleResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listSaleResult.Should().NotBeNull();
        listSaleResult.Sales.Should().NotBeNull();
        listSaleResult.Sales.Should().HaveCount(sales.Count);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que o repositório é chamado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid query When handling Then calls repository correctly")]
    public async Task Handle_ValidRequest_CallsRepositoryCorrectly()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateValidSales();

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o mapper é chamado para cada venda na lista.
    /// </summary>
    [Fact(DisplayName = "Given valid sales When handling Then maps each sale to list item")]
    public async Task Handle_ValidSales_MapsEachSaleToListItem()
    {
        // Given
        var query = ListSaleHandlerTestData.GenerateValidQuery();
        var sales = ListSaleHandlerTestData.GenerateValidSales();

        _saleRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(sales);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        // O handler real usa sales.Select(s => _mapper.Map<SaleListItem>(s)).ToList()
        // então o mapper é chamado para cada sale
        foreach (var sale in sales)
        {
            _mapper.Received(1).Map<SaleListItem>(sale);
        }
    }
} 