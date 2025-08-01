using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.List;

public class ListProductSaleHandler : IRequestHandler<ListProductSaleQuery, ListProductSaleResult>
{
    private readonly IProductSaleRepository _productSaleRepository;
    private readonly IMapper _mapper;

    public ListProductSaleHandler(IProductSaleRepository productSaleRepository, IMapper mapper)
    {
        _productSaleRepository = productSaleRepository;
        _mapper = mapper;
    }

    public async Task<ListProductSaleResult> Handle(ListProductSaleQuery request, CancellationToken cancellationToken)
    {
        var productSales = await _productSaleRepository.ListAsync(cancellationToken);
        return new ListProductSaleResult
        {
            ProductSales = productSales.Select(ps => _mapper.Map<ProductSaleListItem>(ps)).ToList()
        };
    }
} 