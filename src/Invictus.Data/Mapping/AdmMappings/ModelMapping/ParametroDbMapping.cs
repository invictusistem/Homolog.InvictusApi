//using Invictus.Domain.Administrativo.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Data.Mapping.AdmMappings.ModelMapping
//{
//    public class ParametroDbMapping : IEntityTypeConfiguration<Parametros>
//    {
//        public void Configure(EntityTypeBuilder<Parametros> builder)
//        {
//            builder.HasKey(c => c.Id);

//            builder.Property(u => u.Key)
//                .HasColumnType("nvarchar(50)");
            
//            builder.Property(u => u.Value)
//                .HasColumnType("nvarchar(50)");
            
//            builder.Property(u => u.Descricao)
//                .HasColumnType("nvarchar(100)");

//            builder.ToTable("Pametros");
//        }
//    }
//}
