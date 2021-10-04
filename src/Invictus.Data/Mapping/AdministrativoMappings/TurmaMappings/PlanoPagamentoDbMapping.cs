//using Invictus.Domain.Administrativo.TurmaAggregate;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Data.Mapping.AdministrativoMappings.TurmaMappings
//{
//    public class PlanoPagamentoDbMapping : IEntityTypeConfiguration<PlanoPagamento>
//    {
//        public void Configure(EntityTypeBuilder<PlanoPagamento> builder)
//        {
//            builder.HasKey(c => c.Id);

//            builder.Property(d => d.ValorPacote).HasPrecision(11, 2);

//            builder.Property(d => d.BonusMensalidade).HasPrecision(11, 2);

//            builder.Property(d => d.TaxaMatricula).HasPrecision(11, 2);

//            builder.HasOne<Turma>()
//            .WithOne(c => c.PlanoPgm)
//            .HasForeignKey<PlanoPagamento>("TurmaId");

//            builder.ToTable("Planos");

//        }
//    }
//}
