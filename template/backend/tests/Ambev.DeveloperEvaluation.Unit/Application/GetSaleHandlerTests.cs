using Ambev.DeveloperEvaluation.Application.Sale.Get;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe GetSaleHandler.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe GetSaleHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de busca de venda retorna a venda encontrada.
    /// </summary>
    [Fact(DisplayName = "Given valid sale id When getting sale Then returns sale")]
    public async Task Handle_ValidRequest_ReturnsSale()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            DtSale = DateTime.Now,
            Total = 100.00m,
            Discount = 10.00m,
            Cancel = false,
            DtCreate = DateTime.Now,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.NewGuid(),
            IdCreate = Guid.NewGuid(),
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSale>()
        };

        var result = new GetSaleResult
        {
            Id = sale.Id,
            DtSale = sale.DtSale,
            Total = sale.Total,
            Discount = sale.Discount,
            Cancel = sale.Cancel,
            DtCreate = sale.DtCreate,
            DtEdit = sale.DtEdit,
            DtCancel = sale.DtCancel,
            IdCustomer = sale.IdCustomer,
            IdCreate = sale.IdCreate,
            IdEdit = sale.IdEdit,
            IdCancel = sale.IdCancel
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(result);

        // When
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(sale.Id);
        getSaleResult.Total.Should().Be(sale.Total);
        getSaleResult.Discount.Should().Be(sale.Discount);
        getSaleResult.Cancel.Should().Be(sale.Cancel);
        getSaleResult.IdCustomer.Should().Be(sale.IdCustomer);
        await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale id When getting sale Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateCommandWithNonExistentId();

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty sale id When getting sale Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateCommandWithEmptyId();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When handling Then maps sale to result")]
    public async Task Handle_ValidRequest_MapsSaleToResult()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            DtSale = DateTime.Now,
            Total = 100.00m,
            Discount = 10.00m,
            Cancel = false,
            DtCreate = DateTime.Now,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.NewGuid(),
            IdCreate = Guid.NewGuid(),
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSale>()
        };

        var result = new GetSaleResult
        {
            Id = sale.Id,
            DtSale = sale.DtSale,
            Total = sale.Total,
            Discount = sale.Discount,
            Cancel = sale.Cancel,
            DtCreate = sale.DtCreate,
            DtEdit = sale.DtEdit,
            DtCancel = sale.DtCancel,
            IdCustomer = sale.IdCustomer,
            IdCreate = sale.IdCreate,
            IdEdit = sale.IdEdit,
            IdCancel = sale.IdCancel
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(result);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<GetSaleResult>(sale);
    }

    /// <summary>
    /// Testa que o repositório é chamado com o ID correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct id")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectId()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            DtSale = DateTime.Now,
            Total = 100.00m,
            Discount = 10.00m,
            Cancel = false,
            DtCreate = DateTime.Now,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.NewGuid(),
            IdCreate = Guid.NewGuid(),
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSale>()
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna a venda correta.
    /// </summary>
    [Fact(DisplayName = "Given specific sale id When getting sale Then returns correct sale")]
    public async Task Handle_SpecificId_ReturnsCorrectSale()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = GetSaleHandlerTestData.GenerateCommandWithSpecificId(specificId);
        var sale = new Sale
        {
            Id = specificId,
            DtSale = DateTime.Now,
            Total = 250.50m,
            Discount = 25.00m,
            Cancel = false,
            DtCreate = DateTime.Now,
            DtEdit = null,
            DtCancel = null,
            IdCustomer = Guid.NewGuid(),
            IdCreate = Guid.NewGuid(),
            IdEdit = null,
            IdCancel = null,
            ProductSales = new List<ProductSale>()
        };

        var result = new GetSaleResult
        {
            Id = sale.Id,
            DtSale = sale.DtSale,
            Total = sale.Total,
            Discount = sale.Discount,
            Cancel = sale.Cancel,
            DtCreate = sale.DtCreate,
            DtEdit = sale.DtEdit,
            DtCancel = sale.DtCancel,
            IdCustomer = sale.IdCustomer,
            IdCreate = sale.IdCreate,
            IdEdit = sale.IdEdit,
            IdCancel = sale.IdCancel
        };

        _saleRepository.GetByIdAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(result);

        // When
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(specificId);
        getSaleResult.Total.Should().Be(250.50m);
        getSaleResult.Discount.Should().Be(25.00m);
        getSaleResult.Cancel.Should().BeFalse();
    }
} 