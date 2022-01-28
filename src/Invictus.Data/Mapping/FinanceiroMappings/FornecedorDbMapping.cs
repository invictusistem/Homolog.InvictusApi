using Invictus.Domain.Financeiro.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class FornecedorDbMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(c => c.Id);           

            builder.ToTable("Fornecedores");
        }
    }
}