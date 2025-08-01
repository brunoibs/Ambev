using Ambev.DeveloperEvaluation.Application.Sale.Create;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe CreateSaleHandler.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductSaleRepository _productSaleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe CreateSaleHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productSaleRepository = Substitute.For<IProductSaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _productSaleRepository, _productRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de criação de venda é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = command.DtSale,
            Total = command.Total,
            Discount = command.Discount,
            Cancel = command.Cancel,
            DtCreate = command.DtCreate,
            DtEdit = command.DtEdit,
            DtCancel = command.DtCancel,
            IdCustomer = command.IdCustomer,
            IdCreate = command.IdCreate,
            IdEdit = command.IdEdit,
            IdCancel = command.IdCancel,
            ProductSales = new List<ProductSale>()
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Mock para produtos
        foreach (var productSale in command.ProductSales)
        {
            var product = new Product
            {
                Id = productSale.IdProduct,
                Name = "Produto Teste",
                Price = productSale.Price,
                Amount = 10
            };
            _productRepository.GetByIdAsync(productSale.IdProduct, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação inválida de criação de venda lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateInvalidCommand(); // Comando inválido falhará na validação

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com lista de ProductSales vazia lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given sale with empty ProductSales When creating sale Then throws validation exception")]
    public async Task Handle_RequestWithEmptyProductSales_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateCommandWithEmptyProductSales();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com ProductSales inválidos lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given sale with invalid ProductSales When creating sale Then throws validation exception")]
    public async Task Handle_RequestWithInvalidProductSales_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateCommandWithInvalidProductSales();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com o comando correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = command.DtSale,
            Total = command.Total,
            Discount = command.Discount,
            Cancel = command.Cancel,
            DtCreate = command.DtCreate,
            DtEdit = command.DtEdit,
            DtCancel = command.DtCancel,
            IdCustomer = command.IdCustomer,
            IdCreate = command.IdCreate,
            IdEdit = command.IdEdit,
            IdCancel = command.IdCancel,
            ProductSales = new List<ProductSale>()
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Mock para produtos
        foreach (var productSale in command.ProductSales)
        {
            var product = new Product
            {
                Id = productSale.IdProduct,
                Name = "Produto Teste",
                Price = productSale.Price,
                Amount = 10
            };
            _productRepository.GetByIdAsync(productSale.IdProduct, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.DtSale == command.DtSale &&
            c.IdCustomer == command.IdCustomer &&
            c.ProductSales.Count == command.ProductSales.Count));
    }

    /// <summary>
    /// Testa que o resultado é mapeado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When handling Then maps sale to result")]
    public async Task Handle_ValidRequest_MapsSaleToResult()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = command.DtSale,
            Total = command.Total,
            Discount = command.Discount,
            Cancel = command.Cancel,
            DtCreate = command.DtCreate,
            DtEdit = command.DtEdit,
            DtCancel = command.DtCancel,
            IdCustomer = command.IdCustomer,
            IdCreate = command.IdCreate,
            IdEdit = command.IdEdit,
            IdCancel = command.IdCancel,
            ProductSales = new List<ProductSale>()
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Mock para produtos
        foreach (var productSale in command.ProductSales)
        {
            var product = new Product
            {
                Id = productSale.IdProduct,
                Name = "Produto Teste",
                Price = productSale.Price,
                Amount = 10
            };
            _productRepository.GetByIdAsync(productSale.IdProduct, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<CreateSaleResult>(sale);
    }

    /// <summary>
    /// Testa que o repositório é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct entity")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectEntity()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = command.DtSale,
            Total = command.Total,
            Discount = command.Discount,
            Cancel = command.Cancel,
            DtCreate = command.DtCreate,
            DtEdit = command.DtEdit,
            DtCancel = command.DtCancel,
            IdCustomer = command.IdCustomer,
            IdCreate = command.IdCreate,
            IdEdit = command.IdEdit,
            IdCancel = command.IdCancel,
            ProductSales = new List<ProductSale>()
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Mock para produtos
        foreach (var productSale in command.ProductSales)
        {
            var product = new Product
            {
                Id = productSale.IdProduct,
                Name = "Produto Teste",
                Price = productSale.Price,
                Amount = 10
            };
            _productRepository.GetByIdAsync(productSale.IdProduct, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).CreateAsync(
            Arg.Is<Sale>(s => s.IdCustomer == command.IdCustomer),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com dados mínimos válidos é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given minimal valid command When creating sale Then returns success response")]
    public async Task Handle_MinimalValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateMinimalValidCommand();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DtSale = command.DtSale,
            Total = command.Total,
            Discount = command.Discount,
            Cancel = command.Cancel,
            DtCreate = command.DtCreate,
            DtEdit = command.DtEdit,
            DtCancel = command.DtCancel,
            IdCustomer = command.IdCustomer,
            IdCreate = command.IdCreate,
            IdEdit = command.IdEdit,
            IdCancel = command.IdCancel,
            ProductSales = new List<ProductSale>()
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Mock para produtos
        foreach (var productSale in command.ProductSales)
        {
            var product = new Product
            {
                Id = productSale.IdProduct,
                Name = "Produto Teste",
                Price = productSale.Price,
                Amount = 10
            };
            _productRepository.GetByIdAsync(productSale.IdProduct, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
} 