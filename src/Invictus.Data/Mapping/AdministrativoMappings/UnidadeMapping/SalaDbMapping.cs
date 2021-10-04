using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.UnidadeMapping
{
    public class SalaDbMapping : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.Unidade)
            //    .WithMany(c => c.Salas)
            //    .HasForeignKey(m => m.UnidadeId);

            builder.ToTable("Salas");
        }
    }
}
