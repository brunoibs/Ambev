using Ambev.DeveloperEvaluation.Application.Sale.Create;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

/// <summary>
/// Serviço para calcular descontos baseado nas regras de negócio
/// </summary>
public class SaleDiscountService
{
    /// <summary>
    /// Calcula o desconto baseado na quantidade de itens
    /// </summary>
    /// <param name="amount">Quantidade de itens</param>
    /// <returns>Percentual de desconto (0-20)</returns>
    public static decimal CalculateDiscount(int amount)
    {
        // Compras abaixo de 4 itens não podem ter desconto
        if (amount < 4)
            return 0;
            
        // Compras entre 10 e 20 itens idênticos têm 20% de desconto
        if (amount >= 10 && amount <= 20)
            return 20;
            
        // Compras acima de 4 itens idênticos têm 10% de desconto
        if (amount >= 4)
            return 10;
            
        return 0;
    }
    
    /// <summary>
    /// Calcula o total com desconto aplicado
    /// </summary>
    /// <param name="subtotal">Subtotal sem desconto</param>
    /// <param name="discountPercentage">Percentual de desconto</param>
    /// <returns>Total com desconto aplicado</returns>
    public static decimal CalculateTotalWithDiscount(decimal subtotal, decimal discountPercentage)
    {
        var discountAmount = subtotal * (discountPercentage / 100);
        return subtotal - discountAmount;
    }
    
    /// <summary>
    /// Valida se a quantidade está dentro dos limites permitidos
    /// </summary>
    /// <param name="amount">Quantidade de itens</param>
    /// <returns>True se a quantidade é válida</returns>
    public static bool IsValidAmount(int amount)
    {
        return amount > 0 && amount <= 20;
    }
} 