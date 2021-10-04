using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.UnidadeMapping
{
    public class UnidadeDbMapping : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.Descricao)
                .HasColumnName("nvarchar(150)");

            //builder.HasOne(c => c.Modulo)
            //    .WithMany(c => c.Materias)
            //    .HasForeignKey(m => m.ModuloId);

            builder.ToTable("Unidade");
        }
    }
}
