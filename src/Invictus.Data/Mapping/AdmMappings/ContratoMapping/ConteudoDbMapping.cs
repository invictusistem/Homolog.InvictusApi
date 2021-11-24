using Invictus.Domain.Administrativo.ContratoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.ContratoMapping
{
    public class ConteudoDbMapping : IEntityTypeConfiguration<Conteudo>
    {
        public void Configure(EntityTypeBuilder<Conteudo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(cont => cont.Contrato)
                .WithMany(resp => resp.Conteudos)
                .HasForeignKey(resp => resp.ContratoId);

            builder.ToTable("ConteudoContratos");
        }
    }
}
