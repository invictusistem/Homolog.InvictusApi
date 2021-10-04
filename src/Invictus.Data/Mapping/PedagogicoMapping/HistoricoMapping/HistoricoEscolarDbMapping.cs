using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.HistoricoMapping
{
    public class HistoricoEscolarDbMapping : IEntityTypeConfiguration<HistoricoEscolar>
    {
        public void Configure(EntityTypeBuilder<HistoricoEscolar> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("HistoricoEscolar");
        }
    }
}
