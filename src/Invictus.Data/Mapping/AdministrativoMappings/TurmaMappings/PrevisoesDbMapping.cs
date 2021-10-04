using Invictus.Domain.Administrativo.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.TurmaMappings
{
    class PrevisoesDbMapping : IEntityTypeConfiguration<Previsao>
    {
        public void Configure(EntityTypeBuilder<Previsao> builder)
        {


            builder.Property<int>("Id");

            builder.HasKey("Id");

            //builder.IndexerProperty<DateTime>("LastUpdated");

            //builder.Property("Id")// ; ; (p => p..Valor)
            //    .HasColumnType("decimal(18,2)").;
            builder.Property<int>("TurmaId");
            
            //builder.IndexerProperty<int>("TurmaId");
            //builder
            //    .HasOne<Turma>(c => )
            //    .WithOne(h => h.Horarios)
            //    .HasForeignKey("TurmaId");
            ////modelBuilder.Entity<Post>()
            ////.Property<int>("BlogForeignKey");

            //// Use the shadow property as a foreign key
            //modelBuilder.Entity<Post>()
            //    .HasOne(p => p.Blog)
            //    .WithMany(b => b.Posts)
            //    .HasForeignKey("BlogForeignKey");


            

                builder.HasOne<Turma>()
                .WithOne(c => c.Previsoes)
                .HasForeignKey<Previsao>("TurmaId");


            //builder.Property("TurmaId").has// ; ; (p => p..Valor)
            //    .HasColumnType("decimal(18,2)").;

            //builder.Ignore(c => c.Horarios);
            //builder.Ignore(c => c.Previsoes);

            builder.ToTable("Previsoes");

            /*base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");*/
        }
    }
}
