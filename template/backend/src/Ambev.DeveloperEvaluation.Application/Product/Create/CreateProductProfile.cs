using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Product.Create;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Domain.Entities.Product>();
        CreateMap<Domain.Entities.Product, CreateProductResult>();
    }
} 