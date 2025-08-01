using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementação de IProductSaleRepository usando Entity Framework Core
/// </summary>
public class ProductSaleRepository : IProductSaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Inicializa uma nova instância de ProductSaleRepository
    /// </summary>
    /// <param name="context">O contexto do banco de dados</param>
    public ProductSaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria um novo item de venda no banco de dados
    /// </summary>
    /// <param name="productSale">O item de venda a ser criado</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O item de venda criado</returns>
    public async Task<ProductSale> CreateAsync(ProductSale productSale, CancellationToken cancellationToken = default)
    {
        await _context.Set<ProductSale>().AddAsync(productSale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return productSale;
    }

    /// <summary>
    /// Recupera um item de venda pelo seu identificador único
    /// </summary>
    /// <param name="id">O identificador único do item de venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>O item de venda se encontrado, null caso contrário</returns>
    public async Task<ProductSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ProductSale>().FirstOrDefaultAsync(ps => ps.Id == id, cancellationToken);
    }

    /// <summary>
    /// Remove um item de venda do banco de dados
    /// </summary>
    /// <param name="id">O identificador único do item de venda a ser removido</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>True se o item de venda foi removido, false se não encontrado</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productSale = await GetByIdAsync(id, cancellationToken);
        if (productSale == null)
            return false;

        _context.Set<ProductSale>().Remove(productSale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<ProductSale>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<ProductSale>().ToListAsync(cancellationToken);
    }
} 