using Invictus.Domain.Administrativo.PacoteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.PacoteMapping
{
    public class MateriaDbMapping : IEntityTypeConfiguration<PacoteMateria>
    {
        public void Configure(EntityTypeBuilder<PacoteMateria> builder)
        {
            builder.HasKey(c => c.Id);

            // builder.Property(d => d.Modalidade.Value);//.DisplayName).HasColumnType("nvarchar(100)");

            builder.HasOne(c => c.Pacote)
                .WithMany(c => c.Materias)
                .HasForeignKey(m => m.PacoteId);

            builder.ToTable("PacotesMaterias");
        }
    }
}
