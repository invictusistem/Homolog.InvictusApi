using Invictus.Domain.Administrativo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.LogMapping
{
    public class LogTurmasDbMapping : IEntityTypeConfiguration<LogTurmas>
    {
        public void Configure(EntityTypeBuilder<LogTurmas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("LogTurmas");
        }
    }
}
