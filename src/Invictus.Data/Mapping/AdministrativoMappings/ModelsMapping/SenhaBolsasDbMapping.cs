using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
{
    public class SenhaBolsasDbMapping : IEntityTypeConfiguration<SenhaBolsas>
    {
        public void Configure(EntityTypeBuilder<SenhaBolsas> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Turno)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(c => c.DiaDaSemana)
            //    .HasColumnType("nvarchar(50)");
            builder.Property(d => d.PercentualBolsa).HasPrecision(11, 2);
            //builder.HasOne(c => c.Turma)
            //    .WithMany(c => c.Calendarios)
            //    .HasForeignKey(m => m.TurmaId);

            builder.ToTable("SenhaBolsa");
        }
    }
}
