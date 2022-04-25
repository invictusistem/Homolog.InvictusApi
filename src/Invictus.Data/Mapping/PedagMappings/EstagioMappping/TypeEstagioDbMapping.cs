using Invictus.Domain.Padagogico.Estagio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.EstagioMappping
{
    public class TypeEstagioDbMapping : IEntityTypeConfiguration<TypeEstagio>
    {
        public void Configure(EntityTypeBuilder<TypeEstagio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("TypeEstagio");
        }
    }
}
