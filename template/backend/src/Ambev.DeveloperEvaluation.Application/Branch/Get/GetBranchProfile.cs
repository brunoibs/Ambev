using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branch.Get;

public class GetBranchProfile : Profile
{
    public GetBranchProfile()
    {
        CreateMap<Domain.Entities.Branch, GetBranchResult>();
    }
} 