//using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
//using Invictus.Domain.Pedagogico.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Invictus.Data.Mapping.PedagogicoMapping.HistoricoMapping
//{
//    public class ListaPresencaDbMapping : IEntityTypeConfiguration<LivroPresenca>
//    {
//        public void Configure(EntityTypeBuilder<LivroPresenca> builder)
//        {
//            builder.HasKey(c => c.Id);

//            //builder.HasOne(h => h.HistoricoEscolar)
//            //    .WithMany(l => l.ListaPresencas)
//            //    .HasForeignKey(l => l.HistoricoId);

//            builder.ToTable("LivroPresencas");
//        }
//    }
//}
