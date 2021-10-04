//using Invictus.Domain.Administrativo.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Data.Mapping.AdministrativoMappings.ModelsMapping
//{
//    public class PacoteDbMapping : IEntityTypeConfiguration<Pacote>
//    {
//        public void Configure(EntityTypeBuilder<Pacote> builder)
//        {
//            builder.HasKey(c => c.Id);

//            //builder.Property(c => c.Turno)
//            //    .HasColumnType("nvarchar(50)");

//            //builder.Property(c => c.DiaDaSemana)
//            //    .HasColumnType("nvarchar(50)");

//            //builder.HasOne(c => c.Professor)
//            //    .WithMany(c => c.Materias)
//            //    .HasForeignKey(m => m.ProfessorId);

//            builder.ToTable("Pacote");
//        }
//    }
//}
