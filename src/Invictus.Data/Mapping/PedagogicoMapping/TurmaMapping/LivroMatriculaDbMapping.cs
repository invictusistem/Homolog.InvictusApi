using Invictus.Domain.Pedagogico.TurmaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.PedagogicoMapping.TurmaMapping
{
    public class LivroMatriculaDbMapping : IEntityTypeConfiguration<LivroMatricula>
    {
        public void Configure(EntityTypeBuilder<LivroMatricula> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(l => l.Turma)
                .WithOne(l => l.LivroMatriculas)
                .HasForeignKey<LivroMatricula>("TurmaId");
                
            
            //builder.Property(c => c.TurmaId)

            builder.ToTable("LivroMatriculas");
        }
    }
}
