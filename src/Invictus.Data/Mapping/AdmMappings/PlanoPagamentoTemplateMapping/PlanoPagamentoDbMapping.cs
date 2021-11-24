using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.ModelMapping
{
    public class PlanoPagamentoTemplateDbMapping : IEntityTypeConfiguration<PlanoPagamentoTemplate>
    {
        public void Configure(EntityTypeBuilder<PlanoPagamentoTemplate> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(c => c.DiaDaSemana)
            //    .HasColumnType("nvarchar(50)");

            builder.Property(d => d.Valor).HasPrecision(11, 2);

            builder.Property(d => d.TaxaMatricula).HasPrecision(11, 2);
            builder.Property(d => d.ValorMaterial).HasPrecision(11, 2);

            builder.Property(d => d.BonusPontualidade).HasPrecision(11, 2);

            builder.ToTable("PlanoPagamentoTemplate");
        }
    }
}
