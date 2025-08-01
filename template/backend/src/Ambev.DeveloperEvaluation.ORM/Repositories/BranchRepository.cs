using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementação de IBranchRepository usando Entity Framework Core
/// </summary>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância de BranchRepository
    /// </summary>
    /// <param name="context">O contexto do banco de dados</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria uma nova filial no banco de dados
    /// </summary>
    /// <param name="branch">A filial a ser criada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A filial criada</returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
    
        await _context.Set<Branch>().AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Recupera uma filial pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único da filial</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A filial se encontrada, null caso contrário</returns>
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Branch>().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    /// <summary>
    /// Remove uma filial do banco de dados
    /// </summary>
    /// <param name="id">O identificador único da filial a ser removida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se a filial foi removida, false se não encontrada</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Set<Branch>().Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<Branch>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Branch>().ToListAsync(cancellationToken);
    }
}
