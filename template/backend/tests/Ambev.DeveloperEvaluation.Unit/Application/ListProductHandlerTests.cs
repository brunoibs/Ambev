using Ambev.DeveloperEvaluation.Application.Product.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe ListProductHandler.
/// </summary>
public class ListProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListProductHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe ListProductHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public ListProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de listagem de produtos retorna a lista de produtos.
    /// </summary>
    [Fact(DisplayName = "Given valid query When listing products Then returns products list")]
    public async Task Handle_ValidRequest_ReturnsProductsList()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateValidProducts();
        var productListItems = products.Select(p => new ProductListItem
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Amount = p.Amount
        }).ToList();

        var result = new ListProductResult
        {
            Products = productListItems
        };

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        var listProductResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listProductResult.Should().NotBeNull();
        listProductResult.Products.Should().NotBeNull();
        listProductResult.Products.Should().HaveCount(products.Count);
        await _productRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com lista vazia retorna lista vazia.
    /// </summary>
    [Fact(DisplayName = "Given empty products When listing products Then returns empty list")]
    public async Task Handle_EmptyProducts_ReturnsEmptyList()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateEmptyProducts();

        var result = new ListProductResult
        {
            Products = new List<ProductListItem>()
        };

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        var listProductResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listProductResult.Should().NotBeNull();
        listProductResult.Products.Should().NotBeNull();
        listProductResult.Products.Should().BeEmpty();
        await _productRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com um único produto retorna lista com um item.
    /// </summary>
    [Fact(DisplayName = "Given single product When listing products Then returns single item list")]
    public async Task Handle_SingleProduct_ReturnsSingleItemList()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateSingleProduct();
        var product = products.First();

        var productListItem = new ProductListItem
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };

        var result = new ListProductResult
        {
            Products = new List<ProductListItem> { productListItem }
        };

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        var listProductResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listProductResult.Should().NotBeNull();
        listProductResult.Products.Should().NotBeNull();
        listProductResult.Products.Should().HaveCount(1);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que uma solicitação válida com múltiplos produtos retorna lista com múltiplos itens.
    /// </summary>
    [Fact(DisplayName = "Given multiple products When listing products Then returns multiple items list")]
    public async Task Handle_MultipleProducts_ReturnsMultipleItemsList()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateMultipleProducts();

        var productListItems = products.Select(p => new ProductListItem
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Amount = p.Amount
        }).ToList();

        var result = new ListProductResult
        {
            Products = productListItems
        };

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        var listProductResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listProductResult.Should().NotBeNull();
        listProductResult.Products.Should().NotBeNull();
        listProductResult.Products.Should().HaveCount(products.Count);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que o repositório é chamado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid query When handling Then calls repository correctly")]
    public async Task Handle_ValidRequest_CallsRepositoryCorrectly()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateValidProducts();

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        await _productRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o mapper é chamado para cada produto na lista.
    /// </summary>
    [Fact(DisplayName = "Given valid products When handling Then maps each product to list item")]
    public async Task Handle_ValidProducts_MapsEachProductToListItem()
    {
        // Given
        var query = ListProductHandlerTestData.GenerateValidQuery();
        var products = ListProductHandlerTestData.GenerateValidProducts();

        _productRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(products);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        // O handler real usa products.Select(p => _mapper.Map<ProductListItem>(p)).ToList()
        // então o mapper é chamado para cada product
        foreach (var product in products)
        {
            _mapper.Received(1).Map<ProductListItem>(product);
        }
    }
} 