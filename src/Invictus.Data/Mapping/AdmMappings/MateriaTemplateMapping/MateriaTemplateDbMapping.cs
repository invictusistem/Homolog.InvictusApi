using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdmMappings.ModelMapping
{
    public class MateriaTemplateDbMapping : IEntityTypeConfiguration<MateriaTemplate>
    {
        public void Configure(EntityTypeBuilder<MateriaTemplate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("nvarchar(150)");

            builder.Property(p => p.Descricao)
                .HasColumnType("nvarchar(200)");

            builder.HasIndex(m => m.TypePacoteId);

            builder.ToTable("MateriasTemplate");
        }
    }
}
