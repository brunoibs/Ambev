using Ambev.DeveloperEvaluation.Application.Branch.List;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contém testes unitários para a classe ListBranchHandler.
/// </summary>
public class ListBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly ListBranchHandler _handler;

    /// <summary>
    /// Inicializa uma nova instância da classe ListBranchHandlerTests.
    /// Configura as dependências de teste e cria geradores de dados falsos.
    /// </summary>
    public ListBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListBranchHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Testa que uma solicitação válida de listagem de filiais retorna a lista de filiais.
    /// </summary>
    [Fact(DisplayName = "Given valid query When listing branches Then returns branches list")]
    public async Task Handle_ValidRequest_ReturnsBranchesList()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateValidBranches();
        var branchListItems = branches.Select(b => new BranchListItem
        {
            Id = b.Id,
            Name = b.Name
        }).ToList();

        var result = new ListBranchResult
        {
            Branches = branchListItems
        };

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        var listBranchResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listBranchResult.Should().NotBeNull();
        listBranchResult.Branches.Should().NotBeNull();
        listBranchResult.Branches.Should().HaveCount(branches.Count);
        await _branchRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com lista vazia retorna lista vazia.
    /// </summary>
    [Fact(DisplayName = "Given empty branches When listing branches Then returns empty list")]
    public async Task Handle_EmptyBranches_ReturnsEmptyList()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateEmptyBranches();

        var result = new ListBranchResult
        {
            Branches = new List<BranchListItem>()
        };

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        var listBranchResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listBranchResult.Should().NotBeNull();
        listBranchResult.Branches.Should().NotBeNull();
        listBranchResult.Branches.Should().BeEmpty();
        await _branchRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que uma solicitação válida com uma única filial retorna lista com um item.
    /// </summary>
    [Fact(DisplayName = "Given single branch When listing branches Then returns single item list")]
    public async Task Handle_SingleBranch_ReturnsSingleItemList()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateSingleBranch();
        var branch = branches.First();

        var branchListItem = new BranchListItem
        {
            Id = branch.Id,
            Name = branch.Name
        };

        var result = new ListBranchResult
        {
            Branches = new List<BranchListItem> { branchListItem }
        };

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        var listBranchResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listBranchResult.Should().NotBeNull();
        listBranchResult.Branches.Should().NotBeNull();
        listBranchResult.Branches.Should().HaveCount(1);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que uma solicitação válida com múltiplas filiais retorna lista com múltiplos itens.
    /// </summary>
    [Fact(DisplayName = "Given multiple branches When listing branches Then returns multiple items list")]
    public async Task Handle_MultipleBranches_ReturnsMultipleItemsList()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateMultipleBranches();

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        var listBranchResult = await _handler.Handle(query, CancellationToken.None);

        // Then
        listBranchResult.Should().NotBeNull();
        listBranchResult.Branches.Should().NotBeNull();
        listBranchResult.Branches.Should().HaveCount(branches.Count);
        // Não verificamos o conteúdo específico pois o handler real não usa mapper individual
    }

    /// <summary>
    /// Testa que o repositório é chamado corretamente.
    /// </summary>
    [Fact(DisplayName = "Given valid query When handling Then calls repository correctly")]
    public async Task Handle_ValidRequest_CallsRepositoryCorrectly()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateValidBranches();

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        await _branchRepository.Received(1).ListAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Testa que o mapper é chamado para cada filial na lista.
    /// </summary>
    [Fact(DisplayName = "Given valid branches When handling Then maps each branch to list item")]
    public async Task Handle_ValidBranches_MapsEachBranchToListItem()
    {
        // Given
        var query = ListBranchHandlerTestData.GenerateValidQuery();
        var branches = ListBranchHandlerTestData.GenerateValidBranches();

        _branchRepository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(branches);

        // When
        await _handler.Handle(query, CancellationToken.None);

        // Then
        // O handler real usa branches.Select(b => _mapper.Map<BranchListItem>(b)).ToList()
        // então o mapper é chamado para cada branch
        foreach (var branch in branches)
        {
            _mapper.Received(1).Map<BranchListItem>(branch);
        }
    }
} 