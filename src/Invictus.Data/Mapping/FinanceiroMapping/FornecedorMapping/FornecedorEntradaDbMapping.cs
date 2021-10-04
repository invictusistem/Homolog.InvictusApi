using Invictus.Domain.Financeiro.Fornecedor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMapping.FornecedorMapping
{
    public class FornecedorEntradaDbMapping : IEntityTypeConfiguration<FornecedorEntrada>
    {
        public void Configure(EntityTypeBuilder<FornecedorEntrada> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(d => d.ValorPago).HasPrecision(11, 2);
            builder.Property(d => d.Valor).HasPrecision(11, 2);
            builder.Property(d => d.ValorPago).HasPrecision(11, 2);
            builder.Property(d => d.ValorComDesconto).HasPrecision(11, 2);
            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);s
            ////.HasPrecision(11, 6);

            //builder.HasOne(h => h.InformacaoFinanceira)
            //    .WithMany(l => l.Debitos)
            //    .HasForeignKey(l => l.InfoFinancId);

            builder.ToTable("FornecedorEntrada");
        }
    }
}
