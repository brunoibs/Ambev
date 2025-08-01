using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branch.Create;

public class CreateBranchProfile : Profile
{
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Domain.Entities.Branch>();
        CreateMap<Domain.Entities.Branch, CreateBranchResult>();
    }
} 