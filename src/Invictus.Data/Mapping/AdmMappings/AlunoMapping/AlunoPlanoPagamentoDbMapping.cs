using Invictus.Domain.Administrativo.AlunoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.AlunoMapping
{
    public class AlunoPlanoPagamentoDbMapping : IEntityTypeConfiguration<AlunoPlanoPagamento>
    {
        public void Configure(EntityTypeBuilder<AlunoPlanoPagamento> builder)
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

            builder.ToTable("AlunosPlanoPagamento");
        }
    }
}
