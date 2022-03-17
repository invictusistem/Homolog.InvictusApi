using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.ColaboradorMapping
{
    public class AnotacaoColaboradorDbMapping : IEntityTypeConfiguration<AnotacaoColaborador>
    {
        public void Configure(EntityTypeBuilder<AnotacaoColaborador> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.ToTable("ColaboradoresAnotacoes");
        }
    }
}
