using Invictus.Domain.Administrativo.AlunoAggregate;
using Invictus.Domain.Pedagogico.AlunoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagMappings.AlunoMapping
{
    public class AlunoAnotacoesDbMapping : IEntityTypeConfiguration<AlunoAnotacao>
    {
        public void Configure(EntityTypeBuilder<AlunoAnotacao> builder)
        {
            builder.HasKey(c => c.Id);



            builder.ToTable("AlunosAnotacoes");
        }
    }
}
