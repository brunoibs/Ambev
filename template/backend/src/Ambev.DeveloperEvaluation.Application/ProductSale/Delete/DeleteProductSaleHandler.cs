using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Delete;

public class DeleteProductSaleHandler : IRequestHandler<DeleteProductSaleCommand, DeleteProductSaleResponse>
{
    private readonly IProductSaleRepository _productSaleRepository;

    public DeleteProductSaleHandler(IProductSaleRepository productSaleRepository)
    {
        _productSaleRepository = productSaleRepository;
    }

    public async Task<DeleteProductSaleResponse> Handle(DeleteProductSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _productSaleRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"ProductSale with ID {request.Id} not found");

        return new DeleteProductSaleResponse { Success = true };
    }
} 