using Ambev.DeveloperEvaluation.Application.Sale.Delete;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe DeleteSaleHandler.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe DeleteSaleHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    /// <summary>
    /// Testa que uma solicitação válida de exclusão de venda retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid sale id When deleting sale Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteSaleResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteSaleResponse.Should().NotBeNull();
        deleteSaleResponse.Success.Should().BeTrue();
        await _saleRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale id When deleting sale Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateCommandWithNonExistentId();

        _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty sale id When deleting sale Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateCommandWithEmptyId();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o repositório é chamado com o ID correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct id")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectId()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _saleRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given specific sale id When deleting sale Then returns success")]
    public async Task Handle_SpecificId_ReturnsSuccess()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = DeleteSaleHandlerTestData.GenerateCommandWithSpecificId(specificId);

        _saleRepository.DeleteAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteSaleResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteSaleResponse.Should().NotBeNull();
        deleteSaleResponse.Success.Should().BeTrue();
        await _saleRepository.Received(1).DeleteAsync(specificId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o repositório retorna false quando a venda não existe.
    /// </summary>
    [Fact(DisplayName = "Given repository returns false When deleting sale Then throws KeyNotFoundException")]
    public async Task Handle_RepositoryReturnsFalse_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {command.Id} not found");
    }
} 