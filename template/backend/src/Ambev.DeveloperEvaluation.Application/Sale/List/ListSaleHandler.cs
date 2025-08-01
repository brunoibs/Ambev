using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.List;

public class ListSaleHandler : IRequestHandler<ListSaleQuery, ListSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public ListSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<ListSaleResult> Handle(ListSaleQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.ListAsync(cancellationToken);
        return new ListSaleResult
        {
            Sales = sales.Select(s => _mapper.Map<SaleListItem>(s)).ToList()
        };
    }
} 