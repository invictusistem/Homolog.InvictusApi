using Invictus.Domain.Financeiro.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMapping.ProdutoMapping
{
    public class ProdutoDbMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            builder.Property(d => d.Preco).HasPrecision(11, 2);
            builder.Property(d => d.PrecoCusto).HasPrecision(11, 2);

            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            //builder.HasOne(h => h.InformacaoFinanceira)
            //    .WithMany(l => l.Debitos)
            //    .HasForeignKey(l => l.InfoFinancId);

            builder.ToTable("Produto");
        }
    }
}
