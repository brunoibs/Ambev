using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Create;

public class CreateProductSaleHandler : IRequestHandler<CreateProductSaleCommand, CreateProductSaleResult>
{
    private readonly IProductSaleRepository _productSaleRepository;
    private readonly IMapper _mapper;

    public CreateProductSaleHandler(IProductSaleRepository productSaleRepository, IMapper mapper)
    {
        _productSaleRepository = productSaleRepository;
        _mapper = mapper;
    }

    public async Task<CreateProductSaleResult> Handle(CreateProductSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productSale = _mapper.Map<Domain.Entities.ProductSale>(command);
        var createdProductSale = await _productSaleRepository.CreateAsync(productSale, cancellationToken);
        var result = _mapper.Map<CreateProductSaleResult>(createdProductSale);
        return result;
    }
} 