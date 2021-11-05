using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ColaboradorMapping
{
    public class DocumentacaoColaboradorDbMapping : IEntityTypeConfiguration<DocumentacaoColaborador>
    {
        public void Configure(EntityTypeBuilder<DocumentacaoColaborador> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Analisado)
            //    .HasColumnType("nvarchar(10)");

            //builder.Property(c => c.Validado)
            //    .HasColumnType("nvarchar(10)");

            //builder.HasOne(h => h.EstagioMatricula)
            //    .WithMany(l => l.Documentos)
            //    .HasForeignKey(l => l.EstagioMatriculaId);

            builder.ToTable("DocumentacaoColaborador");
        }
    }
}