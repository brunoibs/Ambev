using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.List;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSale;

/// <summary>
/// Profile para mapeamento de ListSale
/// </summary>
public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<Ambev.DeveloperEvaluation.Application.Sale.List.SaleListItem, SaleListItemResponse>();
    }
} 