using Invictus.Domain.Administrativo.UnidadeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.AdmMappings.UnidadeMapping
{
    public class SalaDbMapping : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(u => u.Titulo)
                .HasColumnType("nvarchar(100)");

            builder.Property(u => u.Descricao)
                .HasColumnType("nvarchar(100)");

            builder.Property(u => u.Comentarios)
                .HasColumnType("nvarchar(200)");

            builder.HasOne(c => c.Unidade)
                .WithMany(c => c.Salas)
                .HasForeignKey(m => m.UnidadeId);

            builder.HasIndex(u => u.Descricao);

            builder.ToTable("UnidadesSalas");
        }
    }
}
