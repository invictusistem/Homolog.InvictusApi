using Invictus.Domain.Administrativo.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.TurmaAggregateMapping
{
    public class TurmaDbMaping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(h => h.Previsao)
                .Property(h => h.PrevisaoAtual).HasColumnName("PrevisaoAtual");
            // .HasColumnName("nvarchar(max)");

            builder.OwnsOne(h => h.Previsao)
               .Property(h => h.PrevisaoTerminoAtual).HasColumnName("PrevisaoTerminoAtual");
               //.HasColumnType("nvarchar(8)");

            builder.OwnsOne(h => h.Previsao)
               .Property(h => h.PrevisaoInfo).HasColumnName("PrevisaoInfo");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.Previsao)
               .Property(h => h.DataCriacao).HasColumnName("DataCriacao");
            //.HasColumnName("nvarchar(150)");

            builder.ToTable("Turmas");
        }
    }
}
