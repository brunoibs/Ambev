using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.Get;

public record GetBranchCommand : IRequest<GetBranchResult>
{
    public Guid Id { get; }
    public GetBranchCommand(Guid id)
    {
        Id = id;
    }
} 