using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface para o repositório de ProductSale (item de venda).
/// </summary>
public interface IProductSaleRepository
{
    Task<ProductSale> CreateAsync(ProductSale productSale, CancellationToken cancellationToken = default);
    Task<ProductSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ProductSale>> ListAsync(CancellationToken cancellationToken = default);
} 