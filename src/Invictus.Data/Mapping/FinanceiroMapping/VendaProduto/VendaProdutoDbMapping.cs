using Invictus.Domain.Financeiro.VendaProduto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMapping.VendaProduto
{
    public class VendaProdutoDbMapping : IEntityTypeConfiguration<VendaProdutoAggregate>
    {
        public void Configure(EntityTypeBuilder<VendaProdutoAggregate> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(d => d.).HasPrecision(11, 2);

            builder.Property(d => d.ValorTotalVenda).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            //builder.HasOne<TransacaoBoletoAggregate>()
            //    .WithOne(c => c.Endereco)
            //    .HasForeignKey<TransacaoBoletoEndereco>("TransacaoBoletoId");

            builder.ToTable("VendaProduto");
        }
    }
}
