using Invictus.Domain.Administrativo.Parametros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.ParametrosMapping
{
    public class ParametrosKeyDbMapping : IEntityTypeConfiguration<ParametrosKey>
    {
        public void Configure(EntityTypeBuilder<ParametrosKey> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("ParametrosKey");
        }
    }
}
