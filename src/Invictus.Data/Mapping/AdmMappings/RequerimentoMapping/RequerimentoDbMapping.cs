using Invictus.Domain.Administrativo.RequerimentoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.RequerimentoMapping
{
    public class RequerimentoDbMapping : IEntityTypeConfiguration<Requerimento>
    {
        public void Configure(EntityTypeBuilder<Requerimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Requerimentos");
        }
    }
}