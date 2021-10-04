using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class DocumentoAlunoDbMapping : IEntityTypeConfiguration<DocumentoAluno>
    {
        public void Configure(EntityTypeBuilder<DocumentoAluno> builder)
        {
            builder.HasKey(c => c.Id);

          

            builder.ToTable("Documento_Aluno");
        }
    }
}
