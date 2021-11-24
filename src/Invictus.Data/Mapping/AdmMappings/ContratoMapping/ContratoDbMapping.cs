using Invictus.Domain.Administrativo.ContratosAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.ContratoMapping
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
