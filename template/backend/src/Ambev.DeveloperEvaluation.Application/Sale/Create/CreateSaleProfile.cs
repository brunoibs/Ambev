using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id será gerado pelo banco
            .ForMember(dest => dest.DtSale, opt => opt.MapFrom(src => src.DtSale))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Cancel, opt => opt.MapFrom(src => src.Cancel))
            .ForMember(dest => dest.DtCreate, opt => opt.MapFrom(src => src.DtCreate))
            .ForMember(dest => dest.DtEdit, opt => opt.MapFrom(src => src.DtEdit))
            .ForMember(dest => dest.DtCancel, opt => opt.MapFrom(src => src.DtCancel))
            .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
            .ForMember(dest => dest.IdCreate, opt => opt.MapFrom(src => src.IdCreate))
            .ForMember(dest => dest.IdEdit, opt => opt.MapFrom(src => src.IdEdit))
            .ForMember(dest => dest.IdCancel, opt => opt.MapFrom(src => src.IdCancel))
            .ForMember(dest => dest.IdBranch, opt => opt.MapFrom(src => src.IdBranch))
            .ForMember(dest => dest.ProductSales, opt => opt.Ignore()); // ProductSales serão criados separadamente

        CreateMap<Domain.Entities.Sale, CreateSaleResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DtSale, opt => opt.MapFrom(src => src.DtSale))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
            .ForMember(dest => dest.IdCreate, opt => opt.MapFrom(src => src.IdCreate));
    }
} 