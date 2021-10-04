using Invictus.Domain.Pedagogico.EstagioAggregate;
using Invictus.Domain.Pedagogico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping
{
    public class EstagioMatriculaDbMapping : IEntityTypeConfiguration<EstagioMatricula>
    {
        public void Configure(EntityTypeBuilder<EstagioMatricula> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(h => h.Estagio)
                .WithMany(l => l.Matriculados)
                .HasForeignKey(l => l.EstagioId);

            builder.ToTable("EstagioMatricula");
        }
    }
}
