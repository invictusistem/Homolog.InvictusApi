using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping
{
    public class TurmaPedagDbMapping : IEntityTypeConfiguration<TurmaPedagogico>
    {
        public void Configure(EntityTypeBuilder<TurmaPedagogico> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("TurmaPedagogico");
        }
    }
}
