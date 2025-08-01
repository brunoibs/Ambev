using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Get;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, GetSaleResult>();
    }
} 