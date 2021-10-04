using Invictus.Domain.Pedagogico.EstagioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping
{
    public class DocumentoDbMapping : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Analisado)
            //    .HasColumnType("nvarchar(10)");

            //builder.Property(c => c.Validado)
            //    .HasColumnType("nvarchar(10)");

            //builder.HasOne(h => h.EstagioMatricula)
            //    .WithMany(l => l.Documentos)
            //    .HasForeignKey(l => l.EstagioMatriculaId);

            builder.ToTable("DocumentosEstagio");
        }
    }
}