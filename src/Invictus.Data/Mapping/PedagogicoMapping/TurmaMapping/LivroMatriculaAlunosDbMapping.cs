using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping
{
    public class LivroMatriculaAlunosDbMapping : IEntityTypeConfiguration<LivroMatriculaAlunos>
    {
        public void Configure(EntityTypeBuilder<LivroMatriculaAlunos> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(prof => prof.LivroMatricula)
                .WithMany(mat => mat.Alunos)
                .HasForeignKey(mat => mat.LivroMatriculaId);

            builder.ToTable("LivroMatriculasAlunos");
        }
    }
}
