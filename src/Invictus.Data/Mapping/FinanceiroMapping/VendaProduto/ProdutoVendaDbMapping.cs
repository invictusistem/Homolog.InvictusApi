using Invictus.Domain.Financeiro.VendaProduto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMapping.VendaProduto
{
    public class ProdutoVendaDbMapping : IEntityTypeConfiguration<ProdutoVenda>
    {
        public void Configure(EntityTypeBuilder<ProdutoVenda> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(d => d.).HasPrecision(11, 2);

            builder.Property(d => d.ValorUnitario).HasPrecision(11, 2);
            builder.Property(d => d.ValorTotal).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            //builder.HasOne<TransacaoBoletoAggregate>()
            //    .WithOne(c => c.Endereco)
            //    .HasForeignKey<TransacaoBoletoEndereco>("TransacaoBoletoId");

            builder.ToTable("ProdutosVenda");
        }
    }
}
