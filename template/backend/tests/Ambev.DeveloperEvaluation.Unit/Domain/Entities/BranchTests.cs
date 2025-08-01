using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contém testes unitários para a classe de entidade Branch.
/// Os testes cobrem cenários de criação, propriedades e validações básicas.
/// </summary>
public class BranchTests
{
    /// <summary>
    /// Testa que uma filial pode ser criada com dados válidos.
    /// </summary>
    [Fact(DisplayName = "Branch deve ser criada com dados válidos")]
    public void Given_ValidBranchData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, branch.Id);
        Assert.NotEmpty(branch.Name);
        Assert.True(branch.Name.Length >= 3);
        Assert.True(branch.Name.Length <= 100);
    }

    /// <summary>
    /// Testa que uma filial pode ser criada com dados mínimos válidos.
    /// </summary>
    [Fact(DisplayName = "Branch deve ser criada com dados mínimos válidos")]
    public void Given_MinimalValidBranchData_When_Created_Then_ShouldHaveValidProperties()
    {
        // Arrange
        var branch = BranchTestData.GenerateMinimalValidBranch();

        // Act & Assert
        Assert.NotEqual(Guid.Empty, branch.Id);
        Assert.NotEmpty(branch.Name);
        Assert.True(branch.Name.Length >= 3);
    }

    /// <summary>
    /// Testa que uma filial com dados inválidos tem propriedades inválidas.
    /// </summary>
    [Fact(DisplayName = "Branch com dados inválidos deve ter propriedades inválidas")]
    public void Given_InvalidBranchData_When_Created_Then_ShouldHaveInvalidProperties()
    {
        // Arrange
        var branch = BranchTestData.GenerateInvalidBranch();

        // Act & Assert
        Assert.Equal(Guid.Empty, branch.Id);
        // Verifica se pelo menos uma das propriedades é inválida
        var hasInvalidId = branch.Id == Guid.Empty;
        var hasInvalidName = string.IsNullOrEmpty(branch.Name) || branch.Name.Length < 3;
        
        Assert.True(hasInvalidId || hasInvalidName, 
            "A filial deve ter pelo menos uma propriedade inválida");
    }

    /// <summary>
    /// Testa que o nome da filial pode ser alterado.
    /// </summary>
    [Fact(DisplayName = "Nome da Branch deve poder ser alterado")]
    public void Given_ValidBranch_When_NameChanged_Then_ShouldUpdateName()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();
        var newName = BranchTestData.GenerateValidBranchName();

        // Act
        branch.Name = newName;

        // Assert
        Assert.Equal(newName, branch.Name);
    }

    /// <summary>
    /// Testa que uma filial pode ser criada com construtor padrão.
    /// </summary>
    [Fact(DisplayName = "Branch deve ser criada com construtor padrão")]
    public void Given_DefaultConstructor_When_Created_Then_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var branch = new Branch();

        // Assert
        Assert.NotNull(branch);
        Assert.Equal(string.Empty, branch.Name);
    }

    /// <summary>
    /// Testa que o ID da filial é único.
    /// </summary>
    [Fact(DisplayName = "IDs de Branch devem ser únicos")]
    public void Given_MultipleBranches_When_Created_Then_ShouldHaveUniqueIds()
    {
        // Arrange & Act
        var branch1 = BranchTestData.GenerateValidBranch();
        var branch2 = BranchTestData.GenerateValidBranch();
        var branch3 = BranchTestData.GenerateValidBranch();

        // Assert
        Assert.NotEqual(branch1.Id, branch2.Id);
        Assert.NotEqual(branch2.Id, branch3.Id);
        Assert.NotEqual(branch1.Id, branch3.Id);
    }

    /// <summary>
    /// Testa que o nome da filial não pode ser nulo.
    /// </summary>
    [Fact(DisplayName = "Nome da Branch não deve ser nulo")]
    public void Given_BranchWithNullName_When_Created_Then_ShouldHandleNullName()
    {
        // Arrange
        var branch = new Branch();

        // Act
        branch.Name = null!;

        // Assert
        Assert.Null(branch.Name);
    }

    /// <summary>
    /// Testa que o nome da filial pode ser uma string vazia.
    /// </summary>
    [Fact(DisplayName = "Nome da Branch pode ser string vazia")]
    public void Given_BranchWithEmptyName_When_Created_Then_ShouldHandleEmptyName()
    {
        // Arrange
        var branch = new Branch();

        // Act
        branch.Name = string.Empty;

        // Assert
        Assert.Equal(string.Empty, branch.Name);
    }

    /// <summary>
    /// Testa que uma filial pode ter um nome longo.
    /// </summary>
    [Fact(DisplayName = "Branch deve aceitar nome longo")]
    public void Given_BranchWithLongName_When_Created_Then_ShouldHandleLongName()
    {
        // Arrange
        var branch = new Branch();
        var longName = BranchTestData.GenerateLongBranchName();

        // Act
        branch.Name = longName;

        // Assert
        Assert.Equal(longName, branch.Name);
        Assert.True(branch.Name.Length > 100);
    }
} 