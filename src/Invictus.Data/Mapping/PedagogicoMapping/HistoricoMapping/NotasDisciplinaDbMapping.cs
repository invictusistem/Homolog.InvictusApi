using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.HistoricoMapping
{
    public class NotasDisciplinaDbMapping : IEntityTypeConfiguration<NotasDisciplinas>
    {
        public void Configure(EntityTypeBuilder<NotasDisciplinas> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(h => h.BoletimEscolar)
            //    .WithMany(l => l.Notas)
            //    .HasForeignKey(l => l.BoletimEscolarId);

            //builder.HasOne<BoletimEscolar>()
            //   .WithOne(c => c.)
            //   .HasForeignKey<HorarioBase>("TurmaId");

            builder.ToTable("NotasDisciplinas");
        }
    }
}
