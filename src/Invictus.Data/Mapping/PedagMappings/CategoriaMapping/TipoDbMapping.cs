using Invictus.Domain.Padagogico.Requerimento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.CategoriaMapping
{
    public class TipoDbMapping : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Categoria)
                .WithMany(c => c.Tipos)
                .HasForeignKey(m => m.CategoriaId);

            builder.ToTable("ReqTipos");
        }
    }
}
