using Invictus.Data.Mapping.FinanceiroMappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Data.Mapping
{
    public class FinancModelBuilder
    {
        public static void RegisterBuilders(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoletoDbMapping());
            modelBuilder.ApplyConfiguration(new BolsaDbMapping());
            modelBuilder.ApplyConfiguration(new CaixaDbMapping());
            modelBuilder.ApplyConfiguration(new FornecedorDbMapping());
            modelBuilder.ApplyConfiguration(new InformacaoDebitoDbMapping());
            
            


        }
    }
}
