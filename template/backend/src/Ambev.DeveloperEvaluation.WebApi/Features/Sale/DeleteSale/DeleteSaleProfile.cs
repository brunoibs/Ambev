using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.Delete;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;

/// <summary>
/// Profile para mapeamento de DeleteSale
/// </summary>
public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<Guid, DeleteSaleCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
} 