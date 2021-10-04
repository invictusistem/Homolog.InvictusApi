using Invictus.Domain.Financeiro.Aluno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMapping.InfoFinancMapping
{
    public class InfoFinancDbMapping : IEntityTypeConfiguration<InformacaoFinanceiraAggregate>
    {
        public void Configure(EntityTypeBuilder<InformacaoFinanceiraAggregate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(d => d.ValorCurso).HasPrecision(11, 2);
            //builder.HasOne(h => h.HistoricoEscolar)
            //    .WithMany(l => l.BoletinsEscolares)
            //    .HasForeignKey(l => l.HistoricoId);

            builder.ToTable("InfoFinanceira");
        }
    }
}
