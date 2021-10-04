using Invictus.Domain.Financeiro.Transacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMapping.TransacoesMapping
{
    public class TransacaoBoletoAggDbMapping : IEntityTypeConfiguration<TransacaoBoletoAggregate>
    {
        public void Configure(EntityTypeBuilder<TransacaoBoletoAggregate> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(d => d.).HasPrecision(11, 2);

            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            //builder.HasOne(h => h.InformacaoFinanceira)
            //    .WithMany(l => l.Debitos)
            //    .HasForeignKey(l => l.InfoFinancId);

            builder.ToTable("TransacaoBoleto");
        }
    }
}
