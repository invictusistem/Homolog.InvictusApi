using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping
{
    public class MateriaPedagDbMapping : IEntityTypeConfiguration<MateriaPedag>
    {
        public void Configure(EntityTypeBuilder<MateriaPedag> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(prof => prof.TurmaPedag)
                .WithMany(mat => mat.Materias)
                .HasForeignKey(mat => mat.TurmaPedagId);

            builder.ToTable("MateriasTurmas");
        }
    }
}
