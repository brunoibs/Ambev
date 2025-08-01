using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.List;

public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, SaleListItem>();
    }
} 