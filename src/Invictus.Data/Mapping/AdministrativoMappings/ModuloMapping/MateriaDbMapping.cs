using Invictus.Domain.Administrativo.PacoteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModuloMapping
{
    public class MateriaDbMapping : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.HasKey(c => c.Id);

           // builder.Property(d => d.Modalidade.Value);//.DisplayName).HasColumnType("nvarchar(100)");

            builder.HasOne(c => c.Modulo)
                .WithMany(c => c.Materias)
                .HasForeignKey(m => m.ModuloId);

            builder.ToTable("Materias");
        }
    }
}
