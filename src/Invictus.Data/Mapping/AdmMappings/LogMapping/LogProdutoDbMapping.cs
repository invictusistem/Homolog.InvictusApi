using Invictus.Domain.Administrativo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.AdmMappings.LogMapping
{
    public class LogProdutoDbMapping : IEntityTypeConfiguration<LogProduto>
    {
        public void Configure(EntityTypeBuilder<LogProduto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("LogProdutos");
        }
    }
}
