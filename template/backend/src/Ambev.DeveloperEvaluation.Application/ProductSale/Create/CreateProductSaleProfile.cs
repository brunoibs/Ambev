using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Create;

public class CreateProductSaleProfile : Profile
{
    public CreateProductSaleProfile()
    {
        CreateMap<CreateProductSaleCommand, Domain.Entities.ProductSale>();
        CreateMap<Domain.Entities.ProductSale, CreateProductSaleResult>();
    }
} 