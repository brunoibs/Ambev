using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.Get;

public record GetSaleCommand : IRequest<GetSaleResult>
{
    public Guid Id { get; set; }
    
    public GetSaleCommand() { }
    
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
} 