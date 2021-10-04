using Invictus.Domain.Financeiro.Aluno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMapping.InfoFinancMapping
{
    public class DebitoDbMapping : IEntityTypeConfiguration<Debito>
    {
        public void Configure(EntityTypeBuilder<Debito> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Status)
                .HasColumnType("nvarchar(50)");

            builder.Property(d => d.ValorPago).HasPrecision(11, 2);

            builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
            //.HasPrecision(11, 6);

            builder.HasOne(h => h.InformacaoFinanceiraAggregate)
                .WithMany(l => l.Debitos)
                .HasForeignKey(l => l.InfoFinancId);

            builder.ToTable("Debito");
        }
    }
}
