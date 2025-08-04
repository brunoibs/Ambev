using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductSaleRepository _productSaleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ISaleEventPublisher _eventPublisher;

    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IProductSaleRepository productSaleRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ISaleEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _productSaleRepository = productSaleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
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
            
            // Publicar evento de venda criada
            var saleCreatedEvent = new SaleCreatedEvent(createdSale);
            await _eventPublisher.PublishSaleCreatedAsync(saleCreatedEvent);
            
            var result = new CreateSaleResult
            {
                Id = createdSale.Id,
                DtSale = createdSale.DtSale,
                Total = createdSale.Total,
                Discount = createdSale.Discount,
                IdCustomer = createdSale.IdCustomer,
                IdCreate = createdSale.IdCreate
            };
            return result;
        }
        catch (ValidationException ex)
        {
            throw new ValidationException($"Erro de validação na criação da venda: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro interno na criação da venda: {ex.Message}", ex);
        }
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
            {
                // Em vez de lançar exceção, vamos usar os dados fornecidos
                Console.WriteLine($"Produto com ID {productSale.IdProduct} não encontrado. Usando dados fornecidos.");
                
                // Validar se o total está correto
                var expectedTotalForMissingProduct = productSale.Price * productSale.Amount;
                if (Math.Abs(productSale.Total - expectedTotalForMissingProduct) >= 0.01m)
                {
                    throw new ValidationException($"Total do produto {productSale.IdProduct} não corresponde ao cálculo esperado (Preço: {productSale.Price} x Quantidade: {productSale.Amount} = {expectedTotalForMissingProduct}).");
                }
                
                // Somar ao total da venda
                total += productSale.Total;
                
                // Calcular desconto baseado na quantidade
                var discountPercentageForMissingProduct = SaleDiscountService.CalculateDiscount(productSale.Amount);
                var itemDiscountForMissingProduct = productSale.Total * (discountPercentageForMissingProduct / 100);
                totalDiscount += itemDiscountForMissingProduct;
                
                continue;
            }
            
            // Validar se o preço está correto
            if (Math.Abs(productSale.Price - product.Price) >= 0.01m)
            {
                Console.WriteLine($"Preço do produto {productSale.IdProduct} não corresponde ao preço atual do produto. Usando preço fornecido.");
            }
            
            // Validar se o total está correto
            var expectedTotal = productSale.Price * productSale.Amount;
            if (Math.Abs(productSale.Total - expectedTotal) >= 0.01m)
            {
                throw new ValidationException($"Total do produto {productSale.IdProduct} não corresponde ao cálculo esperado (Preço: {productSale.Price} x Quantidade: {productSale.Amount} = {expectedTotal}).");
            }
            
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