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
    class CentroCustoDbMapping : IEntityTypeConfiguration<CentroCusto>
    {
        public void Configure(EntityTypeBuilder<CentroCusto> builder)
        {
            builder.HasKey(c => c.Id);
                       

            builder.ToTable("CentroCusto");
        }
    }
}
