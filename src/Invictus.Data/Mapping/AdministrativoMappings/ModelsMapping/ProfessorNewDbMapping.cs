using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class ProfessorNewDbMapping : IEntityTypeConfiguration<ProfessorNew>
    {
        public void Configure(EntityTypeBuilder<ProfessorNew> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(c => c.DiaDaSemana)
            //    .HasColumnType("nvarchar(50)");

            //builder.HasOne(c => c.Turma)
            //    .WithMany(c => c.Calendarios)
            //    .HasForeignKey(m => m.TurmaId);

            builder.ToTable("ProfessorNew");
        }
    }
}
