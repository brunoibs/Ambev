using MediatR;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Delete;

public record DeleteProductSaleCommand : IRequest<DeleteProductSaleResponse>
{
    public Guid Id { get; }
    public DeleteProductSaleCommand(Guid id)
    {
        Id = id;
    }
} 