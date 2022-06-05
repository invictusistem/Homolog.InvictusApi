using Invictus.Domain.Administrativo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.AdmMappings.ProdutoMapping
{
    public class ProdutoLogDbMapping : IEntityTypeConfiguration<ProdutoLog>
    {
        public void Configure(EntityTypeBuilder<ProdutoLog> builder)
        {
            builder.HasKey(c => c.Id);            

            builder.ToTable("ProdutosLog");
        }
    }
}
