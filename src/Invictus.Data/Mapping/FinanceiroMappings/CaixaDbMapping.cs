using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Bolsas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class CaixaDbMapping : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Caixa");
        }
    }
}