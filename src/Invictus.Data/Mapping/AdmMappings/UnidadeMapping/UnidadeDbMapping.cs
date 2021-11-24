using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.UnidadeMapping
{
    public class UnidadeDbMapping : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.Sigla)
                .HasColumnType("nvarchar(3)");

            builder.Property(u => u.CNPJ)
                .HasColumnType("nvarchar(14)");

            builder.Property(u => u.Descricao)
                .HasColumnType("nvarchar(150)");

            builder.OwnsOne(h => h.Endereco)
                .Property(h => h.Bairro).HasColumnName("Bairro");
               // .HasColumnName("nvarchar(max)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.CEP).HasColumnName("CEP")
               .HasColumnType("nvarchar(8)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.Complemento).HasColumnName("Complemento");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.Logradouro).HasColumnName("Logradouro");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.Numero).HasColumnName("Numero");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.Cidade).HasColumnName("Cidade");
               //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.Endereco)
               .Property(h => h.UF).HasColumnName("UF")
               .HasColumnType("nvarchar(2)");

            builder.HasIndex(u => u.Sigla);

            builder.ToTable("Unidades");
        }
    }
}
