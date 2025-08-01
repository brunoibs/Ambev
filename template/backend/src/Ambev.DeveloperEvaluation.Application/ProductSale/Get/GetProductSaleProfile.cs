using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Get;

public class GetProductSaleProfile : Profile
{
    public GetProductSaleProfile()
    {
        CreateMap<Domain.Entities.ProductSale, GetProductSaleResult>();
    }
} 