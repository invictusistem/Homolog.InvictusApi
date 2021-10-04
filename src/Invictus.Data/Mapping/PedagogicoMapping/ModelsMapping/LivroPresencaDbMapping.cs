using Invictus.Domain.Pedagogico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.PedagogicoMapping.ModelsMapping
{
    public class LivroPresencaDbMapping : IEntityTypeConfiguration<LivroPresenca>
    {
        public void Configure(EntityTypeBuilder<LivroPresenca> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(h => h.HistoricoEscolar)
            //    .WithMany(l => l.ListaPresencas)
            //    .HasForeignKey(l => l.HistoricoId);

            builder.ToTable("LivroPresencas");
        }
    }
}
