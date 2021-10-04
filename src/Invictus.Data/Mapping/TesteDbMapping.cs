using Invictus.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping
{
    class TesteDbMapping : IEntityTypeConfiguration<TestandoEnum>
    {
        public void Configure(EntityTypeBuilder<TestandoEnum> builder)
        {
            builder.HasKey(c => c.Descricao);

            builder.Property(c => c.Cardtype)
                .HasColumnType("nvarchar(100)");

            builder.ToTable("Testando");
        }
    }
}
