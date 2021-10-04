using Invictus.Domain.Administrativo.ContratosAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ContratoMapping
{
    public class ContratoDbMapping : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(aluno => aluno.Aluno)
            //    .WithMany(resp => resp.Responsaveis)
            //    .HasForeignKey(resp => resp.AlunoId);

            builder.ToTable("Contratos");
        }
    }
}
