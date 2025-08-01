using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.Get;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

/// <summary>
/// Profile para mapeamento de GetSale
/// </summary>
public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Guid, GetSaleCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
        CreateMap<GetSaleResult, GetSaleResponse>();
    }
} 