using Invictus.Domain.Padagogico.Estagio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.EstagioMappping
{
    public class EstagioDbMapping : IEntityTypeConfiguration<Estagio>
    {
        public void Configure(EntityTypeBuilder<Estagio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Estagios");
        }
    }
}
