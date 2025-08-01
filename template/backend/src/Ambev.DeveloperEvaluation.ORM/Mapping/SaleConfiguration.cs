using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.DtSale).HasColumnName("Dt_Sale").IsRequired();
        builder.Property(s => s.Total).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.Discount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.Cancel).HasDefaultValue(false);
        builder.Property(s => s.DtCreate).HasColumnName("Dt_Create").IsRequired();
        builder.Property(s => s.DtEdit).HasColumnName("Dt_Edit");
        builder.Property(s => s.DtCancel).HasColumnName("Dt_Cancel");
        builder.Property(s => s.IdCustomer).IsRequired();
        builder.Property(s => s.IdCreate).IsRequired();
        builder.Property(s => s.IdEdit);
        builder.Property(s => s.IdCancel);

        // Configurar relacionamento com ProductSale com delete em cascata
        builder.HasMany(s => s.ProductSales)
               .WithOne()
               .HasForeignKey(ps => ps.IdSale)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
} 