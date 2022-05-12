using Invictus.Domain.Administrativo.RequerimentoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.RequerimentoMapping
{
    public class TipoRequerimentoDbMapping : IEntityTypeConfiguration<TipoRequerimento>
    {
        public void Configure(EntityTypeBuilder<TipoRequerimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("TypeRequerimento");
        }
    }
}