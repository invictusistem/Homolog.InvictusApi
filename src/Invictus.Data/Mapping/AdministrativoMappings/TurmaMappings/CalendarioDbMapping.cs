using Invictus.Domain.Administrativo.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.TurmaMappings
{
    public class CalendarioDbMapping : IEntityTypeConfiguration<Calendario>
    {
        public void Configure(EntityTypeBuilder<Calendario> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            builder.HasIndex(c => c.DiaAula);

            builder.Property(c => c.DiaDaSemana)
                .HasColumnType("nvarchar(50)");

            builder.HasOne(c => c.Turma)
                .WithMany(c => c.Calendarios)
                .HasForeignKey(m => m.TurmaId);

            builder.ToTable("Calendarios");
        }
    }
}
