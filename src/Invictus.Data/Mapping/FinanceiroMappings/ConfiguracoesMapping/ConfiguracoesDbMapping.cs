using Invictus.Domain.Financeiro.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMappings.ConfiguracoesMapping
{
    public class BancoDbMapping : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.HasKey(c => c.Id);
                      
            //builder.HasOne(c => c.b)
            //    .WithMany(c => c.Boletos)
            //    .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("Bancos");
        }
    }

    public class CentroCustoDbMapping : IEntityTypeConfiguration<CentroCusto>
    {
        public void Configure(EntityTypeBuilder<CentroCusto> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.b)
            //    .WithMany(c => c.Boletos)
            //    .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("CentroCustos");
        }
    }

    public class FormaRecebimentoDbMapping : IEntityTypeConfiguration<FormaRecebimento>
    {
        public void Configure(EntityTypeBuilder<FormaRecebimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(d => d.Taxa).HasPrecision(11, 2);

            //builder.HasOne(c => c.b)
            //    .WithMany(c => c.Boletos)
            //    .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("FormasRecebimento");
        }
    }

    public class MeioPagamentoDbMapping : IEntityTypeConfiguration<MeioPagamento>
    {
        public void Configure(EntityTypeBuilder<MeioPagamento> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.b)
            //    .WithMany(c => c.Boletos)
            //    .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("MeioPagamentos");
        }
    }

    public class PlanoContaDbMapping : IEntityTypeConfiguration<PlanoConta>
    {
        public void Configure(EntityTypeBuilder<PlanoConta> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.pl.b)
            //    .WithMany(c => c.Boletos)
            //    .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("PlanoContas");
        }
    }

    public class SubContaDbMapping : IEntityTypeConfiguration<SubConta>
    {
        public void Configure(EntityTypeBuilder<SubConta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.PlanoConta)
                .WithMany(c => c.Subcontas)
                .HasForeignKey(m => m.PlanoContaId);

            builder.ToTable("SubContas");
        }
    }
}
