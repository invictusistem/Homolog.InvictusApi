using Invictus.Domain.Pedagogico.Responsaveis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.ResponsavelMapping
{
    public class ResponsavelDbMapping : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.CPF)
                .HasColumnType("nvarchar(11)");

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

            builder.ToTable("Responsaveis");
        }
    }
}
