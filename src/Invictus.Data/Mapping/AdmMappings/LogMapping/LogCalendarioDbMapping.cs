using Invictus.Domain.Administrativo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.LogMapping
{
    public class LogCalendarioDbMapping : IEntityTypeConfiguration<LogCalendario>
    {
        public void Configure(EntityTypeBuilder<LogCalendario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("LogCalendarios");
        }
    }
}
