using Invictus.Domain.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class ReparceladoDbMapping : IEntityTypeConfiguration<Reparcelado>
    {
        public void Configure(EntityTypeBuilder<Reparcelado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Reparcelados");
        }
    }
}
