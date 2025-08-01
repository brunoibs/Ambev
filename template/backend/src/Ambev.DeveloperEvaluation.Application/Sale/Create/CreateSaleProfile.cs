using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
        CreateMap<Domain.Entities.Sale, CreateSaleResult>();
    }
} 