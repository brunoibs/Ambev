using Ambev.DeveloperEvaluation.Application.Branch.Create;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe CreateBranchHandler.
/// </summary>
public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly CreateBranchHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe CreateBranchHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateBranchHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de criação de filial é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given valid branch data When creating branch Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação inválida de criação de filial lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch data When creating branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateInvalidCommand(); // Comando vazio falhará na validação

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que uma solicitação com nome muito longo lança uma exceção de validação.
    /// </summary>
    [Fact(DisplayName = "Given branch with long name When creating branch Then throws validation exception")]
    public async Task Handle_RequestWithLongName_ThrowsValidationException()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateCommandWithLongName();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Testa que o mapper é chamado com o comando correto.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to branch entity")]
    public async Task Handle_ValidRequest_MapsCommandToBranch()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Branch>(Arg.Is<CreateBranchCommand>(c =>
            c.Name == command.Name));
    }

    /// <summary>
    /// Testa que o resultado é mapeado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid branch When handling Then maps branch to result")]
    public async Task Handle_ValidRequest_MapsBranchToResult()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<CreateBranchResult>(branch);
    }

    /// <summary>
    /// Testa que o repositório é chamado com a entidade correta.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then calls repository with correct entity")]
    public async Task Handle_ValidRequest_CallsRepositoryWithCorrectEntity()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _branchRepository.Received(1).CreateAsync(
            Arg.Is<Branch>(b => b.Name == command.Name),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação com dados mínimos válidos é processada com sucesso.
    /// </summary>
    [Fact(DisplayName = "Given minimal valid command When creating branch Then returns success response")]
    public async Task Handle_MinimalValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateMinimalValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }
} 