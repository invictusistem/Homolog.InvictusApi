using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class MateriasDaTurmaDbMapping : IEntityTypeConfiguration<MateriasDaTurma>
    {
        public void Configure(EntityTypeBuilder<MateriasDaTurma> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(c => c.DiaDaSemana)
            //    .HasColumnType("nvarchar(50)");

            builder.HasOne(c => c.Professor)
                .WithMany(c => c.Materias)
                .HasForeignKey(m => m.ProfessorId);

            builder.ToTable("MateriasDaTurma");
        }
    }
}
