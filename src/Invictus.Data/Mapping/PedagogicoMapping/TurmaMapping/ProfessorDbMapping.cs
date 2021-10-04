using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping
{
    public class ProfessorDbMapping : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(t => t.Materia)
                .WithMany(prof => prof.Professores)
                .HasForeignKey(turma => turma.MateriaId);

            builder.ToTable("Professores");
        }
    }
}
