using Ambev.DeveloperEvaluation.Application.Product.Get;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe GetProductHandler.
/// </summary>
public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe GetProductHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de busca de produto retorna o produto encontrado.
    /// </summary>
    [Fact(DisplayName = "Given valid product id When getting product Then returns product")]
    public async Task Handle_ValidRequest_ReturnsProduct()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = command.Id,
            Name = "Produto Teste",
            Price = 10.00m,
            Amount = 5
        };

        var result = new GetProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(product);
        _mapper.Map<GetProductResult>(product).Returns(result);

        // When
        var getProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getProductResult.Should().NotBeNull();
        getProductResult.Id.Should().Be(product.Id);
        getProductResult.Name.Should().Be(product.Name);
        getProductResult.Price.Should().Be(product.Price);
        getProductResult.Amount.Should().Be(product.Amount);
        await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product id When getting product Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentProduct_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateCommandWithNonExistentId();

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty product id When getting product Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateCommandWithEmptyId();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid product When handling Then maps product to result")]
    public async Task Handle_ValidRequest_MapsProductToResult()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = command.Id,
            Name = "Produto Teste",
            Price = 10.00m,
            Amount = 5
        };

        var result = new GetProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(product);
        _mapper.Map<GetProductResult>(product).Returns(result);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<GetProductResult>(product);
    }

    /// <summary>
    /// Testa que o repositório é chamado com o ID correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct id")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectId()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = command.Id,
            Name = "Produto Teste",
            Price = 10.00m,
            Amount = 5
        };

        _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna o produto correto.
    /// </summary>
    [Fact(DisplayName = "Given specific product id When getting product Then returns correct product")]
    public async Task Handle_SpecificId_ReturnsCorrectProduct()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = GetProductHandlerTestData.GenerateCommandWithSpecificId(specificId);
        var product = new Product
        {
            Id = specificId,
            Name = "Produto Específico",
            Price = 25.50m,
            Amount = 10
        };

        var result = new GetProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };

        _productRepository.GetByIdAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(product);
        _mapper.Map<GetProductResult>(product).Returns(result);

        // When
        var getProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getProductResult.Should().NotBeNull();
        getProductResult.Id.Should().Be(specificId);
        getProductResult.Name.Should().Be("Produto Específico");
        getProductResult.Price.Should().Be(25.50m);
        getProductResult.Amount.Should().Be(10);
    }
} 