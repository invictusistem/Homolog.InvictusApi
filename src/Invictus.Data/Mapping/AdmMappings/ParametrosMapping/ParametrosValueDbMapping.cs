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
    public class ParametrosValueDbMapping : IEntityTypeConfiguration<ParametrosValue>
    {
        public void Configure(EntityTypeBuilder<ParametrosValue> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.Value)
                .HasColumnType("nvarchar(50)");

            builder.Property(u => u.Descricao)
                .HasColumnType("nvarchar(100)");

            builder.HasOne(c => c.ParametrosKey)
                .WithMany(c => c.ParametrosValue)
                .HasForeignKey(m => m.ParametrosKeyId);

            builder.ToTable("ParametrosValue");
        }
    }
}
