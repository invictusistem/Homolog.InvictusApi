using Invictus.Domain.Financeiro.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMapping.Configuracoes
{
    public class SubContaDbMapping : IEntityTypeConfiguration<SubConta>
    {
        public void Configure(EntityTypeBuilder<SubConta> builder)
        {
            builder.HasKey(c => c.Id);


            builder.ToTable("SubConta");
        }
    }
}
