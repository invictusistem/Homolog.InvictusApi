//using Invictus.Domain.Financeiro.Aluno;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Data.Mapping.FinanceiroMapping.InfoFinancMapping
//{
//    public class ValorEntradaDbMapping : IEntityTypeConfiguration<ValorEntrada>
//    {
//        public void Configure(EntityTypeBuilder<ValorEntrada> builder)
//        {
//            builder.HasKey(c => c.Id);

//            builder.HasOne(h => h.InformacaoFinanceiraAggregate)
//                .WithOne(l => l.Entrada)
//                .HasForeignKey<ValorEntrada>("InformacaoFinanceiraId");

//            builder.Property(d => d.Valor).HasPrecision(11, 2);

//            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
//            // .HasPrecision(11, 2);
//            builder.ToTable("ValorEntrada");
//        }
//    }
//}
