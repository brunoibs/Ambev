using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface para o repositório de Branch (filial).
/// </summary>
public interface IBranchRepository
{
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Branch>> ListAsync(CancellationToken cancellationToken = default);
}


