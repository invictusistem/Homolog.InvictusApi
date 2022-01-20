using Invictus.Domain.Administrativo.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.TurmaAggregateMapping
{
    public class PresencaDbMapping : IEntityTypeConfiguration<Presenca>
    {
        public void Configure(EntityTypeBuilder<Presenca> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("TurmasPresencas");
        }
    }
}
