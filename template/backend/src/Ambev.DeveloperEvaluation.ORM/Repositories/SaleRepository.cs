using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementação de ISaleRepository usando Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância de SaleRepository
    /// </summary>
    /// <param name="context">O contexto do banco de dados</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria uma nova venda no banco de dados
    /// </summary>
    /// <param name="sale">A venda a ser criada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A venda criada</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Set<Sale>().AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Recupera uma venda pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único da venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>A venda se encontrada, null caso contrário</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Sale>().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Remove uma venda do banco de dados
    /// </summary>
    /// <param name="id">O identificador único da venda a ser removida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se a venda foi removida, false se não encontrada</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            // PRIMEIRO: Deletar todos os ProductSales relacionados usando a coluna SaleId
            var productSales = await _context.Set<ProductSale>()
                .Where(ps => EF.Property<Guid>(ps, "SaleId") == id)
                .ToListAsync(cancellationToken);
            
            if (productSales.Any())
            {
                _context.Set<ProductSale>().RemoveRange(productSales);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // SEGUNDO: Buscar e deletar a venda
            var sale = await _context.Set<Sale>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
                
            if (sale == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return false;
            }

            _context.Set<Sale>().Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            
            // Commit da transação
            await transaction.CommitAsync(cancellationToken);
            return true;
        }
        catch
        {
            // Rollback em caso de erro
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<Sale>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Sale>().ToListAsync(cancellationToken);
    }
} 