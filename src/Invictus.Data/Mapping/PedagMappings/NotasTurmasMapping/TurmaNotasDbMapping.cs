using Invictus.Domain.Padagogico.NotasTurmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.PedagMappings.NotasTurmasMapping
{
    public class TurmaNotasDbMapping : IEntityTypeConfiguration<TurmaNotas>
    {
        public void Configure(EntityTypeBuilder<TurmaNotas> builder)
        {
            builder.HasKey(c => c.Id);

           

            builder.ToTable("TurmasNotas");
        }
    }
}
