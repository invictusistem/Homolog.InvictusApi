using Invictus.Domain.Administrativo.FuncionarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.FuncionarioMapping
{
    public class FuncionarioDbMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasIndex(f => f.Id)
                .HasDatabaseName("Id")
                .IsUnique();

            builder.HasIndex(c => c.TipoPessoa);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            //builder.HasOne(c => c.Endereco)
            //   .WithOne(c => c.Funcionario);
               

            builder.ToTable("Funcionarios");
        }
    }
}
