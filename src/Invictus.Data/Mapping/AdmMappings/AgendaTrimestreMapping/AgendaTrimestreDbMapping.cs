using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.AgendaTrimestreMapping
{
    public class AgendaTrimestreDbMapping : IEntityTypeConfiguration<AgendaTrimestre>
    {
        public void Configure(EntityTypeBuilder<AgendaTrimestre> builder)
        {
            builder.HasKey(c => c.Id);
                       

            builder.ToTable("AgendasTrimestre");
        }
    }
}
