using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.DtSale).NotEmpty();
        RuleFor(s => s.IdCustomer).NotEmpty();
        RuleFor(s => s.IdCreate).NotEmpty();
        
        // Regras de negócio para ProductSales
        RuleFor(s => s.ProductSales)
            .NotEmpty().WithMessage("A venda deve ter pelo menos um item.")
            .Must(ValidateProductSales).WithMessage("Os itens da venda não atendem às regras de negócio.");
    }
    
    private bool ValidateProductSales(List<Domain.Entities.ProductSale> productSales)
    {
        if (productSales == null || !productSales.Any())
            return false;
            
        foreach (var productSale in productSales)
        {
            // Não é possível vender mais de 20 itens idênticos
            if (productSale.Amount > 20)
                return false;
                
            // Quantidade deve ser maior que zero
            if (productSale.Amount <= 0)
                return false;
                
            // Preço deve ser maior que zero
            if (productSale.Price <= 0)
                return false;
                
            // Total deve ser maior que zero
            if (productSale.Total <= 0)
                return false;
                
            // Total deve corresponder ao cálculo (Preço x Quantidade)
            var expectedTotal = productSale.Price * productSale.Amount;
            if (productSale.Total != expectedTotal)
                return false;
        }
        
        return true;
    }
} 