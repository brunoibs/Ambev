using Ambev.DeveloperEvaluation.Application.Product.Delete;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe DeleteProductHandler.
/// </summary>
public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe DeleteProductHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProductHandler(_productRepository);
    }

    /// <summary>
    /// Testa que uma solicitação válida de exclusão de produto retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid product id When deleting product Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteProductResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteProductResponse.Should().NotBeNull();
        deleteProductResponse.Success.Should().BeTrue();
        await _productRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent product id When deleting product Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentProduct_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateCommandWithNonExistentId();

        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty product id When deleting product Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateCommandWithEmptyId();

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
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _productRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given specific product id When deleting product Then returns success")]
    public async Task Handle_SpecificId_ReturnsSuccess()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = DeleteProductHandlerTestData.GenerateCommandWithSpecificId(specificId);

        _productRepository.DeleteAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteProductResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteProductResponse.Should().NotBeNull();
        deleteProductResponse.Success.Should().BeTrue();
        await _productRepository.Received(1).DeleteAsync(specificId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o repositório retorna false quando o produto não existe.
    /// </summary>
    [Fact(DisplayName = "Given repository returns false When deleting product Then throws KeyNotFoundException")]
    public async Task Handle_RepositoryReturnsFalse_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {command.Id} not found");
    }
} 