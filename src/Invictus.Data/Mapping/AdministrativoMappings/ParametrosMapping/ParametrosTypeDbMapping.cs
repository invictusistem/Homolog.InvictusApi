using Invictus.Domain.Administrativo.Parametros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.AdministrativoMappings.ParametrosMapping
{
    public class ParametrosTypeDbMapping : IEntityTypeConfiguration<ParametrosType>
    {
        public void Configure(EntityTypeBuilder<ParametrosType> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.Unidade)
            //    .WithMany(c => c.Modulos)
            //    .HasForeignKey(m => m.UnidadeId);

            

            builder.ToTable("ParametrosType");
        }
    }
}
