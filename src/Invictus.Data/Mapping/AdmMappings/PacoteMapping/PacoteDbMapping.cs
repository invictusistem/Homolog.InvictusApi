using Invictus.Domain.Administrativo.PacoteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.PacoteMapping
{
    public class PacoteDbMapping : IEntityTypeConfiguration<Pacote>
    {
        public void Configure(EntityTypeBuilder<Pacote> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.Unidade)
            //    .WithMany(c => c.Modulos)
            //    .HasForeignKey(m => m.UnidadeId);

            //builder.Property(d => d.Preco).HasPrecision(11, 2);

            builder.ToTable("Pacotes");
        }
    }
}
