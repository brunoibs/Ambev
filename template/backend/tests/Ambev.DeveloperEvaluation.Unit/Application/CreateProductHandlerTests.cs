using Ambev.DeveloperEvaluation.Application.Product.Create;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe CreateProductHandler.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe CreateProductHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de criação de produto é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };

        var result = new CreateProductResult
        {
            Id = product.Id
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação inválida de criação de produto lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateInvalidCommand(); // Comando inválido falhará na validação

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com nome muito longo lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given product with long name When creating product Then throws validation exception")]
    public async Task Handle_RequestWithLongName_ThrowsValidationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateCommandWithLongName();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com preço zero lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given product with zero price When creating product Then throws validation exception")]
    public async Task Handle_RequestWithZeroPrice_ThrowsValidationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateCommandWithZeroPrice();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com quantidade negativa lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given product with negative amount When creating product Then throws validation exception")]
    public async Task Handle_RequestWithNegativeAmount_ThrowsValidationException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateCommandWithNegativeAmount();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com o comando correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };

        _mapper.Map<Product>(command).Returns(product);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
            c.Name == command.Name &&
            c.Price == command.Price &&
            c.Amount == command.Amount));
    }

    /// <summary>
    /// Testa que o resultado é mapeado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid product When handling Then maps product to result")]
    public async Task Handle_ValidRequest_MapsProductToResult()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };

        var result = new CreateProductResult
        {
            Id = product.Id
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<CreateProductResult>(product);
    }

    /// <summary>
    /// Testa que o repositório é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct entity")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectEntity()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };

        _mapper.Map<Product>(command).Returns(product);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _productRepository.Received(1).CreateAsync(
            Arg.Is<Product>(p => p.Name == command.Name && 
                                p.Price == command.Price && 
                                p.Amount == command.Amount),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com dados mínimos válidos é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given minimal valid command When creating product Then returns success response")]
    public async Task Handle_MinimalValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateMinimalValidCommand();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };

        var result = new CreateProductResult
        {
            Id = product.Id
        };

        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
} 