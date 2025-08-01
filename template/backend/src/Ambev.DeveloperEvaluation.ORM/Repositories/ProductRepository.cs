using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementação de IProductRepository usando Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância de ProductRepository
    /// </summary>
    /// <param name="context">O contexto do banco de dados</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria um novo produto no banco de dados
    /// </summary>
    /// <param name="product">O produto a ser criado</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O produto criado</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Set<Product>().AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Recupera um produto pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único do produto</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O produto se encontrado, null caso contrário</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <summary>
    /// Remove um produto do banco de dados
    /// </summary>
    /// <param name="id">O identificador único do produto a ser removido</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se o produto foi removido, false se não encontrado</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Set<Product>().Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<Product>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>().ToListAsync(cancellationToken);
    }
} 