using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping.FinanceiroMappings
{
    public class FinancModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoletoDbMapping());
            modelBuilder.ApplyConfiguration(new InformacaoDebitoDbMapping());
            modelBuilder.ApplyConfiguration(new BolsaDbMapping());
            modelBuilder.ApplyConfiguration(new FornecedorDbMapping());


        }
    }
}
