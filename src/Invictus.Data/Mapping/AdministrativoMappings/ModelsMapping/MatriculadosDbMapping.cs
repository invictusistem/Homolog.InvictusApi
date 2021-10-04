using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class MateriasDaTurmaMapping : IEntityTypeConfiguration<Matriculados>
    {
        public void Configure(EntityTypeBuilder<Matriculados> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(aluno => aluno.Aluno)
            //    .WithMany(resp => resp.Responsaveis)
            //    .HasForeignKey(resp => resp.AlunoId);

            builder.ToTable("Matriculados");
        }
    }
}
