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
    public class LogMatriculasDbMapping : IEntityTypeConfiguration<LogMatriculas>
    {
        public void Configure(EntityTypeBuilder<LogMatriculas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("LogMatriculas");
        }
    }
}
