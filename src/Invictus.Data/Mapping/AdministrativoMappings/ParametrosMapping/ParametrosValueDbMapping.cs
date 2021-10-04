using Invictus.Domain.Administrativo.Parametros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ParametrosMapping
{
    public class ParametrosValueDbMapping : IEntityTypeConfiguration<ParametrosValue>
    {
        public void Configure(EntityTypeBuilder<ParametrosValue> builder)
        {
            builder.HasKey(c => c.Id);

            // builder.Property(d => d.Modalidade.Value);//.DisplayName).HasColumnType("nvarchar(100)");
            builder.HasIndex(p => p.Nome);

            builder.HasOne(c => c.ParametrosType)
                .WithMany(c => c.ParametrosValues)
                .HasForeignKey(m => m.ParametrosTypeId);

            builder.ToTable("ParametrosValue");
        }
    }
}
