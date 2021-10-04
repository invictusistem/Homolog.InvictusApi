using Invictus.Domain.Pedagogico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping
{
    class AgendaProvasDbMapping : IEntityTypeConfiguration<ProvasAgenda>
    {
        public void Configure(EntityTypeBuilder<ProvasAgenda> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("AgendaProvas");
        }
    }
}
