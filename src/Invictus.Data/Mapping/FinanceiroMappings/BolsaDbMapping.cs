using Invictus.Domain.Financeiro.Bolsas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class BolsaDbMapping : IEntityTypeConfiguration<Bolsa>
    {
        public void Configure(EntityTypeBuilder<Bolsa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Bolsas");
        }
    }
}