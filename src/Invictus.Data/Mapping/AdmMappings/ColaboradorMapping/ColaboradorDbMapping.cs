using Invictus.Domain.Administrativo.ColaboradorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.AdmMappings.ColaboradorMapping
{
    public class ColaboradorDbMapping : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.CPF)
                .HasColumnType("nvarchar(11)");

            builder.Property(u => u.Celular)
                .HasColumnType("nvarchar(12)");

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

            builder.ToTable("Colaboradores");
        }
    }
}
