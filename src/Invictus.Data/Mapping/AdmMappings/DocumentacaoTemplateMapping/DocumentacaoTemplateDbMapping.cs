using Invictus.Domain.Administrativo.DocumentacaoTemplateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.DocumentacaoTemplateMapping
{
    public class DocumentacaoTemplateDbMapping : IEntityTypeConfiguration<DocumentacaoTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentacaoTemplate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(a => a.Nome);

            builder.ToTable("DocumentacaoTemplate");
        }
    }
}