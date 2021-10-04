using Invictus.Domain.Administrativo.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdministrativoMappings.TurmaMappings
{
    public class TurmaDbMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(c => c.Id);


            //builder.Property(p => p.Unidade)
            //    .HasColumnType("nvarchar(50)");

            builder.Property(p => p.StatusDaTurma)
                .HasColumnType("nvarchar(50)");

            //builder.Property(p => p.Turno)
            //    .HasColumnType("nvarchar(50)");
            //    .HasColumnType("decimal(18,2)");

            builder.Ignore(c => c.Horarios);
            builder.Ignore(c => c.Previsoes);

            builder.ToTable("Turma");

            /*base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");*/
        }
    }
}
