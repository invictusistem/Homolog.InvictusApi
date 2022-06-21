using Invictus.Domain.Administrativo.FuncionarioAggregate;
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
    public class DisponibilidadeDbMapping : IEntityTypeConfiguration<Disponibilidade>
    {
        public void Configure(EntityTypeBuilder<Disponibilidade> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("ProfessoresDisponibilidades");
        }
    }
}