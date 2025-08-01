using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductSaleRepository _productSaleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IProductSaleRepository productSaleRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productSaleRepository = productSaleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        // Validação já é feita automaticamente pelo ValidationBehavior
        // var validator = new CreateSaleValidator();
        // var validationResult = await validator.ValidateAsync(command, cancellationToken);
        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        // Validar e calcular total da venda baseado nos ProductSales
        var (total, discount) = await ValidateAndCalculateTotal(command.ProductSales, cancellationToken);
        
        // Atualizar o comando com os valores calculados
        command.Total = total;
        command.Discount = discount;
        command.DtCreate = DateTime.UtcNow;
        
        // Criar a venda
        var sale = _mapper.Map<Domain.Entities.Sale>(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        
        // Criar os itens de venda (ProductSales)
        await CreateSaleItems(createdSale.Id, command.ProductSales, cancellationToken);
        
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
    
    private async Task<(decimal total, decimal discount)> ValidateAndCalculateTotal(
        List<Domain.Entities.ProductSale> productSales, 
        CancellationToken cancellationToken)
    {
        decimal total = 0;
        decimal totalDiscount = 0;
        
        foreach (var productSale in productSales)
        {
            // Validar se o produto existe
            var product = await _productRepository.GetByIdAsync(productSale.IdProduct, cancellationToken);
            if (product == null)
                throw new ValidationException($"Produto com ID {productSale.IdProduct} não encontrado.");
            
            // Validar se o preço está correto
            if (productSale.Price != product.Price)
                throw new ValidationException($"Preço do produto {productSale.IdProduct} não corresponde ao preço atual do produto.");
            
            // Validar se o total está correto
            var expectedTotal = productSale.Price * productSale.Amount;
            if (productSale.Total != expectedTotal)
                throw new ValidationException($"Total do produto {productSale.IdProduct} não corresponde ao cálculo esperado (Preço: {productSale.Price} x Quantidade: {productSale.Amount} = {expectedTotal}).");
            
            // Somar ao total da venda
            total += productSale.Total;
            
            // Calcular desconto baseado na quantidade (se houver regras de desconto)
            var discountPercentage = SaleDiscountService.CalculateDiscount(productSale.Amount);
            var itemDiscount = productSale.Total * (discountPercentage / 100);
            totalDiscount += itemDiscount;
        }
        
        var finalTotal = total - totalDiscount;
        return (finalTotal, totalDiscount);
    }
    
    private async Task CreateSaleItems(Guid saleId, List<Domain.Entities.ProductSale> productSales, CancellationToken cancellationToken)
    {
        foreach (var productSale in productSales)
        {
            // Criar um novo objeto ProductSale para evitar conflitos de chave primária
            var newProductSale = new Domain.Entities.ProductSale
            {
                IdSale = saleId,
                IdProduct = productSale.IdProduct,
                Amount = productSale.Amount,
                Price = productSale.Price,
                Total = productSale.Total
            };
            
            // Garantir que o IdSale foi definido corretamente
            if (newProductSale.IdSale == Guid.Empty)
            {
                throw new InvalidOperationException($"IdSale não foi definido corretamente para o produto {newProductSale.IdProduct}");
            }
            
            await _productSaleRepository.CreateAsync(newProductSale, cancellationToken);
        }
    }
} 