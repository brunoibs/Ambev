using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Product.Get;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Domain.Entities.Product, GetProductResult>();
    }
} 