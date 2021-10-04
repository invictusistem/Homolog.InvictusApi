using Invictus.Domain.Pedagogico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping
{
    class EstagioDbMapping : IEntityTypeConfiguration<Estagio>
    {
        public void Configure(EntityTypeBuilder<Estagio> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Estagio");
        }
    }
}
