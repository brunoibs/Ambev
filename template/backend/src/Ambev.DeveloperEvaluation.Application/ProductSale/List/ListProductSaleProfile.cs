using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.List;

public class ListProductSaleProfile : Profile
{
    public ListProductSaleProfile()
    {
        CreateMap<Domain.Entities.ProductSale, ProductSaleListItem>();
    }
} 