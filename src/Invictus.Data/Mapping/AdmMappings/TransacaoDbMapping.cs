using Invictus.Core.Enumerations;
using Invictus.Domain.Administrativo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Invictus.Data.Mapping.AdmMappings
{
    public class TransacaoDbMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasKey(c => c.Tipo);

            //builder.Property<TipoTrans>(c => c.Tipo)
            //    .HasConversion(new EnumToStringConverter<TipoTrans>());

            //builder.Property(e => e.Tipo)
            //.HasConversion(x => (int)x, x => (TipoTrans)x);           

            builder.ToTable("Transacoes");
        }
    }
}
