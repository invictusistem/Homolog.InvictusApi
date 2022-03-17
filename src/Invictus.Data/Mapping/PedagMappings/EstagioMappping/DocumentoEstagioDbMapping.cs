using Invictus.Domain.Padagogico.Estagio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.PedagMappings.EstagioMappping
{
    public class DocumentoEstagioDbMapping : IEntityTypeConfiguration<DocumentoEstagio>
    {
        public void Configure(EntityTypeBuilder<DocumentoEstagio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.MatriculaEstagio)
               .WithMany(c => c.Documentos)
               .HasForeignKey(m => m.MatriculaEstagioId);

            builder.ToTable("EstagioDocumentos");
        }
    }
}
