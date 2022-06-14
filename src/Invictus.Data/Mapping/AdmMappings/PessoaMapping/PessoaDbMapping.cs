using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.FuncionarioMapping
{
    public class PessoaDbMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(c => c.Id);
            //builder.HasIndex(f => f.Id)
            //    .HasDatabaseName("Id")
            //    .IsUnique();

            builder.HasIndex(c => c.TipoPessoa);

            //builder.Property(c => c.Id)
            //    .ValueGeneratedNever();

            //builder.HasOne(c => c.Endereco)
            //   .WithOne(c => c.Funcionario);
               

            builder.ToTable("Pessoas");
        }
    }
}
