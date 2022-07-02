using Invictus.Domain.Padagogico.Requerimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.CategoriaMapping
{
    public class CategoriaDbMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("ReqCategorias");
        }
    }
}
