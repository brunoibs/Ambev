using Ambev.DeveloperEvaluation.Application.Branch.Delete;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe DeleteBranchHandler.
/// </summary>
public class DeleteBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly DeleteBranchHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe DeleteBranchHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public DeleteBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new DeleteBranchHandler(_branchRepository);
    }

    /// <summary>
    /// Testa que uma solicitação válida de exclusão de filial retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid branch id When deleting branch Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateValidCommand();

        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteBranchResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteBranchResponse.Should().NotBeNull();
        deleteBranchResponse.Success.Should().BeTrue();
        await _branchRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent branch id When deleting branch Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentBranch_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateCommandWithNonExistentId();

        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty branch id When deleting branch Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateCommandWithEmptyId();

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
        var command = DeleteBranchHandlerTestData.GenerateValidCommand();

        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _branchRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna sucesso.
    /// </summary>
    [Fact(DisplayName = "Given specific branch id When deleting branch Then returns success")]
    public async Task Handle_SpecificId_ReturnsSuccess()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = DeleteBranchHandlerTestData.GenerateCommandWithSpecificId(specificId);

        _branchRepository.DeleteAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteBranchResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteBranchResponse.Should().NotBeNull();
        deleteBranchResponse.Success.Should().BeTrue();
        await _branchRepository.Received(1).DeleteAsync(specificId, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o repositório retorna false quando a filial não existe.
    /// </summary>
    [Fact(DisplayName = "Given repository returns false When deleting branch Then throws KeyNotFoundException")]
    public async Task Handle_RepositoryReturnsFalse_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateValidCommand();

        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {command.Id} not found");
    }
} 