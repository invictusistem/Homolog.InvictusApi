using Invictus.Domain.Administrativo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.LogMapping
{
    public class LogBoletoDbMapping : IEntityTypeConfiguration<LogBoletos>
    {
        public void Configure(EntityTypeBuilder<LogBoletos> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("LogBoletos");
        }
    }
}
