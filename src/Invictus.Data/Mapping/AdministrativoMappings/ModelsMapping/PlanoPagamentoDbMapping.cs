using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class PlanoPagamentoDbMapping : IEntityTypeConfiguration<PlanoPagamento>
    {
        public void Configure(EntityTypeBuilder<PlanoPagamento> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(c => c.DiaDaSemana)
            //    .HasColumnType("nvarchar(50)");

            builder.Property(d => d.Valor).HasPrecision(11, 2);

            builder.Property(d => d.TaxaMatricula).HasPrecision(11, 2);

            builder.Property(d => d.BonusMensalidade).HasPrecision(11, 2);

            builder.ToTable("PlanoPagamento");
        }
    }
}
