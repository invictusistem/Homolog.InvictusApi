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
    public class AlunoDocumentoDbMapping : IEntityTypeConfiguration<AlunoDocumento>
    {
        public void Configure(EntityTypeBuilder<AlunoDocumento> builder)
        {
            builder.HasKey(c => c.Id);           

            builder.ToTable("AlunosDocumentos");
        }
    }
}
