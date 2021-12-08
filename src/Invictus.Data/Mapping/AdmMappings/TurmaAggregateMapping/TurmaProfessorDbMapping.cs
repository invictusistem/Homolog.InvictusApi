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
    public class TurmaProfessorDbMapping : IEntityTypeConfiguration<TurmaProfessor>
    {
        public void Configure(EntityTypeBuilder<TurmaProfessor> builder)
        {
            builder.HasKey(c => c.Id);

            

            builder.ToTable("TurmasProfessores");
        }
    }
}
