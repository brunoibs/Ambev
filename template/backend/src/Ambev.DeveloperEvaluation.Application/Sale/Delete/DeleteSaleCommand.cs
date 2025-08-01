using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.Delete;

public record DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    public Guid Id { get; set; }
    
    public DeleteSaleCommand() { }
    
    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }
} 