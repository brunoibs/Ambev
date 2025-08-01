using Ambev.DeveloperEvaluation.Application.Sale.Create;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

/// <summary>
/// Exemplos de uso das regras de negócio para vendas
/// </summary>
public static class SaleBusinessRulesExample
{
    /// <summary>
    /// Exemplos de como as regras de negócio são aplicadas
    /// </summary>
    public static void ShowExamples()
    {
        Console.WriteLine("=== Regras de Negócio - Exemplos ===\n");
        
        // Exemplo 1: Compra com menos de 4 itens (sem desconto)
        Console.WriteLine("Exemplo 1: Compra de 3 Skols");
        var discount1 = SaleDiscountService.CalculateDiscount(3);
        var total1 = SaleDiscountService.CalculateTotalWithDiscount(30.0m, discount1); // 3 * R$ 10,00
        Console.WriteLine($"Quantidade: 3 | Desconto: {discount1}% | Total: R$ {total1:F2}\n");
        
        // Exemplo 2: Compra com 4-9 itens (10% de desconto)
        Console.WriteLine("Exemplo 2: Compra de 5 Guarás");
        var discount2 = SaleDiscountService.CalculateDiscount(5);
        var total2 = SaleDiscountService.CalculateTotalWithDiscount(35.0m, discount2); // 5 * R$ 7,00
        Console.WriteLine($"Quantidade: 5 | Desconto: {discount2}% | Total: R$ {total2:F2}\n");
        
        // Exemplo 3: Compra com 10-20 itens (20% de desconto)
        Console.WriteLine("Exemplo 3: Compra de 15 Águas");
        var discount3 = SaleDiscountService.CalculateDiscount(15);
        var total3 = SaleDiscountService.CalculateTotalWithDiscount(45.0m, discount3); // 15 * R$ 3,00
        Console.WriteLine($"Quantidade: 15 | Desconto: {discount3}% | Total: R$ {total3:F2}\n");
        
        // Exemplo 4: Tentativa de compra com mais de 20 itens (inválido)
        Console.WriteLine("Exemplo 4: Tentativa de compra de 25 Sucos (INVÁLIDO)");
        var isValid = SaleDiscountService.IsValidAmount(25);
        Console.WriteLine($"Quantidade: 25 | Válido: {isValid}\n");
        
        Console.WriteLine("=== Resumo das Regras ===");
        Console.WriteLine("• 1-3 itens: Sem desconto");
        Console.WriteLine("• 4-9 itens: 10% de desconto");
        Console.WriteLine("• 10-20 itens: 20% de desconto");
        Console.WriteLine("• Mais de 20 itens: Não permitido");
    }
} 