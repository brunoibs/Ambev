using Ambev.DeveloperEvaluation.Application.Branch.Get;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe GetBranchHandler.
/// </summary>
public class GetBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly GetBranchHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe GetBranchHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public GetBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetBranchHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de busca de filial retorna a filial encontrada.
    /// </summary>
    [Fact(DisplayName = "Given valid branch id When getting branch Then returns branch")]
    public async Task Handle_ValidRequest_ReturnsBranch()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = command.Id,
            Name = "Filial Teste"
        };

        var result = new GetBranchResult
        {
            Id = branch.Id,
            Name = branch.Name
        };

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(branch);
        _mapper.Map<GetBranchResult>(branch).Returns(result);

        // When
        var getBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getBranchResult.Should().NotBeNull();
        getBranchResult.Id.Should().Be(branch.Id);
        getBranchResult.Name.Should().Be(branch.Name);
        await _branchRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID que não existe lança uma exceção KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existent branch id When getting branch Then throws KeyNotFoundException")]
    public async Task Handle_NonExistentBranch_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateCommandWithNonExistentId();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Branch?)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {command.Id} not found");
    }

    /// <summary>
    /// Testa que uma solicitação com ID vazio lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given empty branch id When getting branch Then throws validation exception")]
    public async Task Handle_EmptyId_ThrowsValidationException()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateCommandWithEmptyId();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid branch When handling Then maps branch to result")]
    public async Task Handle_ValidRequest_MapsBranchToResult()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = command.Id,
            Name = "Filial Teste"
        };

        var result = new GetBranchResult
        {
            Id = branch.Id,
            Name = branch.Name
        };

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(branch);
        _mapper.Map<GetBranchResult>(branch).Returns(result);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<GetBranchResult>(branch);
    }

    /// <summary>
    /// Testa que o repositório é chamado com o ID correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct id")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectId()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = command.Id,
            Name = "Filial Teste"
        };

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _branchRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com ID específico retorna a filial correta.
    /// </summary>
    [Fact(DisplayName = "Given specific branch id When getting branch Then returns correct branch")]
    public async Task Handle_SpecificId_ReturnsCorrectBranch()
    {
        // Given
        var specificId = Guid.NewGuid();
        var command = GetBranchHandlerTestData.GenerateCommandWithSpecificId(specificId);
        var branch = new Branch
        {
            Id = specificId,
            Name = "Filial Específica"
        };

        var result = new GetBranchResult
        {
            Id = branch.Id,
            Name = branch.Name
        };

        _branchRepository.GetByIdAsync(specificId, Arg.Any<CancellationToken>())
            .Returns(branch);
        _mapper.Map<GetBranchResult>(branch).Returns(result);

        // When
        var getBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getBranchResult.Should().NotBeNull();
        getBranchResult.Id.Should().Be(specificId);
        getBranchResult.Name.Should().Be("Filial Específica");
    }
} 