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
    public class HorariosDbMapping : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Turma)
               .WithMany(c => c.Horarios)
               .HasForeignKey(m => m.TurmaId);

            builder.ToTable("TurmasHorarios");
        }
    }
}
