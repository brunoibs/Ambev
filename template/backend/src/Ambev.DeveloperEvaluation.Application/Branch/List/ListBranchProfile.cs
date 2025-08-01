using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.List;

public class ListBranchProfile : Profile
{
    public ListBranchProfile()
    {
        CreateMap<Domain.Entities.Branch, BranchListItem>();
    }
} 