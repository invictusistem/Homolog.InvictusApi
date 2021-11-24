using Invictus.Domain.Administrativo.PacoteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.PacoteMapping
{
    public class DocumentaoExigenciaDbMapping : IEntityTypeConfiguration<DocumentacaoExigencia>
    {
        public void Configure(EntityTypeBuilder<DocumentacaoExigencia> builder)
        {
            builder.HasKey(c => c.Id);

            // builder.Property(d => d.Modalidade.Value);//.DisplayName).HasColumnType("nvarchar(100)");

            builder.HasOne(c => c.Pacote)
                .WithMany(c => c.DocumentosExigidos)
                .HasForeignKey(m => m.PacoteId);

            builder.ToTable("PacotesDocumentacao");
        }
    }
}
