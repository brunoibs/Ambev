using MediatR;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Get;

public record GetProductSaleCommand : IRequest<GetProductSaleResult>
{
    public Guid Id { get; }
    public GetProductSaleCommand(Guid id)
    {
        Id = id;
    }
} 