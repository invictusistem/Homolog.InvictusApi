using Invictus.Domain.Financeiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class BoletoDbMapping : IEntityTypeConfiguration<Boleto>
    {
        public void Configure(EntityTypeBuilder<Boleto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(d => d.Valor).HasPrecision(11, 2);
            builder.Property(d => d.ValorPago).HasPrecision(11, 2);

            builder.HasIndex(d => d.StatusBoleto);

            builder.HasIndex(d => d.CentroCustoUnidadeId);
            // index no status
            // Index no centroCusto
            ///
            builder.OwnsOne(h => h.InfoBoletos)
                .Property(h => h.Id_unico).HasColumnName("Id_unico");
            // .HasColumnName("nvarchar(max)");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Id_unico_original).HasColumnName("Id_unico_original")
               .HasColumnType("nvarchar(8)");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Status).HasColumnName("Status");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Msg).HasColumnName("Msg");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Nossonumero).HasColumnName("Nossonumero");
            //.HasColumnName("nvarchar(150)");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.LinkBoleto).HasColumnName("LinkBoleto");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.LinkGrupo).HasColumnName("LinkGrupo");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.LinhaDigitavel).HasColumnName("LinhaDigitavel");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Pedido_numero).HasColumnName("Pedido_numero");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Banco_numero).HasColumnName("Banco_numero");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Token_facilitador).HasColumnName("Token_facilitador");

            builder.OwnsOne(h => h.InfoBoletos)
               .Property(h => h.Credencial).HasColumnName("Credencial");
            //.HasColumnName("nvarchar(150)");           
            builder.HasOne(c => c.InformacaoDebito)
                .WithMany(c => c.Boletos)
                .HasForeignKey(m => m.InformacaoDebitoId);

            builder.ToTable("Boletos");
        }
    }
}
