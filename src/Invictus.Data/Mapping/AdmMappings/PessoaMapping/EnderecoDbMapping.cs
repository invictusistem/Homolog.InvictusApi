using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.FuncionarioMapping
{
    public class EnderecoDbMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Pessoa)
               .WithOne(c => c.Endereco)
               .HasForeignKey<Endereco>(m => m.PessoaId);

            builder.ToTable("Enderecos");
        }
    }
}
