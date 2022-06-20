using Invictus.Domain.Administrativo.ProfessorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.ProfessorMapping
{
    public class MateriaHabilitadaDbMapping : IEntityTypeConfiguration<MateriaHabilitada>
    {
        public void Configure(EntityTypeBuilder<MateriaHabilitada> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Professor)
               .WithMany(c => c.Materias)
               .HasForeignKey(m => m.ProfessorId);

            builder.ToTable("ProfessoresMaterias");
        }
    }
}