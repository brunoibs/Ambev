using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.Create;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Profile para mapeamento de CreateSale
/// </summary>
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
            .ForMember(dest => dest.ProductSales, opt => opt.MapFrom(src => src.ProductSales));

        CreateMap<ProductSaleRequest, Domain.Entities.ProductSale>()
            .ForMember(dest => dest.IdSale, opt => opt.Ignore()); // Ignora IdSale pois será definido no handler

        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
} 