using Invictus.Domain.Financeiro.Transacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.FinanceiroMapping.TransacoesMapping
{
    public class TransacaoBoletoEnderecoDbMapping : IEntityTypeConfiguration<TransacaoBoletoEndereco>
    {
        public void Configure(EntityTypeBuilder<TransacaoBoletoEndereco> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(p => p.Status)
            //    .HasColumnType("nvarchar(50)");

            //builder.Property(d => d.).HasPrecision(11, 2);

            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            builder.HasOne<TransacaoBoletoAggregate>()
                .WithOne(c => c.Endereco)
                .HasForeignKey<TransacaoBoletoEndereco>("TransacaoBoletoId");

            builder.ToTable("TransacaoBoletoEndereco");
        }
    }
}
