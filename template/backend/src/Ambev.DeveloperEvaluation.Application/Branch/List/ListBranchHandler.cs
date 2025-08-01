using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.List;

public class ListBranchHandler : IRequestHandler<ListBranchQuery, ListBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public ListBranchHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<ListBranchResult> Handle(ListBranchQuery request, CancellationToken cancellationToken)
    {
        var branches = await _branchRepository.ListAsync(cancellationToken);
        return new ListBranchResult
        {
            Branches = branches.Select(b => _mapper.Map<BranchListItem>(b)).ToList()
        };
    }
} 