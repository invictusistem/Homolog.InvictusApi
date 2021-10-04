using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.HistoricoMapping
{
    public class BoletimDbMapping : IEntityTypeConfiguration<BoletimEscolar>
    {
        public void Configure(EntityTypeBuilder<BoletimEscolar> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(h => h.HistoricoEscolar)
                .WithMany(l => l.BoletinsEscolares)
                .HasForeignKey(l => l.HistoricoId);

            builder.ToTable("BoletinsEscolares");
        }
    }
}
