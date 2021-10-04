using Invictus.Domain.Administrativo.AlunoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.AlunoMapping
{
    class ResponsavelDbMapping : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(aluno => aluno.Aluno)
                .WithMany(resp => resp.Responsaveis)
                .HasForeignKey(resp => resp.AlunoId);

            builder.ToTable("Responsaveis");
        }
    }
}
