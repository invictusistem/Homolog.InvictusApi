using Invictus.Domain.Administrativo.UnidadeAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.AutorizacaoMapping
{
    public class AutorizacaoDbMapping : IEntityTypeConfiguration<Autorizacao>
    {
        public void Configure(EntityTypeBuilder<Autorizacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(a => a.UsuarioId);

            builder.ToTable("Autorizacoes");
        }
    }
}