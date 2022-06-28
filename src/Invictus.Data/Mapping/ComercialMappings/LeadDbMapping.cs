using Invictus.Domain.Comercial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.ComercialMappings
{
    public class LeadDbMapping : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Leads");
        }
    }
}