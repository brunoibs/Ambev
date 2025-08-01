using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.List;

public class ListProductProfile : Profile
{
    public ListProductProfile()
    {
        CreateMap<Domain.Entities.Product, ProductListItem>();
    }
} 