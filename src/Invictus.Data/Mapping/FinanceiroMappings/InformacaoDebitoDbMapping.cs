//using Invictus.Domain.Financeiro;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Invictus.Data.Mapping.FinanceiroMappings
//{
//    public class InformacaoDebitoDbMapping : IEntityTypeConfiguration<InformacaoDebito>
//    {
//        public void Configure(EntityTypeBuilder<InformacaoDebito> builder)
//        {
//            builder.HasKey(c => c.Id);

//            builder.Property(d => d.ValorTotal).HasPrecision(11, 2);
//            //builder.Property(d => d.ValorPago).HasPrecision(11, 2);

//            builder.ToTable("InformacoesDebitos");
//        }
//    }
//}
