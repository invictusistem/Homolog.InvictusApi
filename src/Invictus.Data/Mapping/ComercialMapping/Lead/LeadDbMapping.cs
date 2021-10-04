using Microsoft.EntityFrameworkCore;
using Invictus.Domain.Comercial.Leads;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invictus.Data.Mapping.ComercialMapping.Leads
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
