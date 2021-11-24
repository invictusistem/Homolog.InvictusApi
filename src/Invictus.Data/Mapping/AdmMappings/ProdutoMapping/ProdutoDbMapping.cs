using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.ModelMapping
{
    public class ProdutoDbMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("nvarchar(100)");

            builder.Property(p => p.Descricao)
                .HasColumnType("nvarchar(200)");

            builder.Property(p => p.Observacoes)
                .HasColumnType("nvarchar(400)");

            builder.Property(d => d.Preco).HasPrecision(11, 2);
            builder.Property(d => d.PrecoCusto).HasPrecision(11, 2);

            //builder.Property(d => d.ValorTitulo).HasPrecision(11, 2);
            ////.HasPrecision(11, 6);

            //builder.HasOne(h => h.InformacaoFinanceira)
            //    .WithMany(l => l.Debitos)
            //    .HasForeignKey(l => l.InfoFinancId);

            builder.ToTable("Produtos");
        }
    }
}
