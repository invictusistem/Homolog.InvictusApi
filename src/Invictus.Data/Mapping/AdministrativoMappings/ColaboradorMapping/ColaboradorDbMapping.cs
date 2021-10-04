using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.ColaboradorMapping
{
    public class ColaboradorDbMapping : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Colaborador");
        }
    }
}
