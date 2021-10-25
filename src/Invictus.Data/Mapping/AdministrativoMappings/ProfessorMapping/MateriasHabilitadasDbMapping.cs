using Invictus.Domain.Administrativo.ProfessorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invictus.Data.Mapping.AdministrativoMappings.ProfessorMapping
{
    public class MateriasHabilitadasDbMapping : IEntityTypeConfiguration<MateriasHabilitadas>
    {
        public void Configure(EntityTypeBuilder<MateriasHabilitadas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("MateriasHabilitadas");
        }
    }
}
