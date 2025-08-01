using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductSaleConfiguration : IEntityTypeConfiguration<ProductSale>
{
    public void Configure(EntityTypeBuilder<ProductSale> builder)
    {
        builder.ToTable("Product_Sales");

        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id).HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();

        // Mapeamento da coluna IdSales (plural)
        builder.Property(ps => ps.IdSale).HasColumnName("IdSales").IsRequired();
        
        // Mapeamento da coluna SaleId que está causando o erro de foreign key
        builder.Property("SaleId").HasColumnName("SaleId");
        
        builder.Property(ps => ps.IdProduct).IsRequired();
        builder.Property(ps => ps.Amount).IsRequired();
        builder.Property(ps => ps.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(ps => ps.Total).HasColumnType("decimal(18,2)").IsRequired();
    }
} 