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
            .ForMember(dest => dest.DtSale, opt => opt.MapFrom(src => src.DtSale))
            .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
            .ForMember(dest => dest.IdCreate, opt => opt.MapFrom(src => src.IdCreate))
            .ForMember(dest => dest.IdBranch, opt => opt.MapFrom(src => src.IdBranch))
            .ForMember(dest => dest.ProductSales, opt => opt.MapFrom(src => src.ProductSales))
            .ForMember(dest => dest.Total, opt => opt.Ignore()) // Será calculado no handler
            .ForMember(dest => dest.Discount, opt => opt.Ignore()) // Será calculado no handler
            .ForMember(dest => dest.DtCreate, opt => opt.Ignore()) // Será definido no handler
            .ForMember(dest => dest.Cancel, opt => opt.Ignore())
            .ForMember(dest => dest.DtEdit, opt => opt.Ignore())
            .ForMember(dest => dest.DtCancel, opt => opt.Ignore())
            .ForMember(dest => dest.IdEdit, opt => opt.Ignore())
            .ForMember(dest => dest.IdCancel, opt => opt.Ignore());

        CreateMap<ProductSaleRequest, Domain.Entities.ProductSale>()
            .ForMember(dest => dest.IdSale, opt => opt.Ignore()) // Ignora IdSale pois será definido no handler
            .ForMember(dest => dest.IdProduct, opt => opt.MapFrom(src => src.IdProduct))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

        CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
            .ForMember(dest => dest.IdCreate, opt => opt.MapFrom(src => src.IdCreate))
            .ForMember(dest => dest.DtSale, opt => opt.MapFrom(src => src.DtSale))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));
    }
} 