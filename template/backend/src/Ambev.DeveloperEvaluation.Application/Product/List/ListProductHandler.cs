using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.List;

public class ListProductHandler : IRequestHandler<ListProductQuery, ListProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListProductResult> Handle(ListProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListAsync(cancellationToken);
        return new ListProductResult
        {
            Products = products.Select(p => _mapper.Map<ProductListItem>(p)).ToList()
        };
    }
} 