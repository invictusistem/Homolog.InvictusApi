using Invictus.Domain.Padagogico.Estagio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.EstagioMappping
{
    public class MatriculaEstagioDbMapping : IEntityTypeConfiguration<MatriculaEstagio>
    {
        public void Configure(EntityTypeBuilder<MatriculaEstagio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("EstagiosMatriculas");
        }
    }
}
