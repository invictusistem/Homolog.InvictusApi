using Invictus.Domain.Administrativo.Calendarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.CalendarioMapping
{
    public class CalendarioDbMapping : IEntityTypeConfiguration<Calendario>
    {
        public void Configure(EntityTypeBuilder<Calendario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(a => a.DiaAula);

            //builder.HasIndex(a => a.DiaAula);

            builder.ToTable("Calendarios");
        }
    }
}