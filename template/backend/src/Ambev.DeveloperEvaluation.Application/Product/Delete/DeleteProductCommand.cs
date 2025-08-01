using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.Delete;

public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    public Guid Id { get; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
} 
