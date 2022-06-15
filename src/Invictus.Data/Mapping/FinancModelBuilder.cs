using Invictus.Data.Mapping.FinanceiroMappings;
using Invictus.Data.Mapping.FinanceiroMappings.ConfiguracoesMapping;
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
            modelBuilder.ApplyConfiguration(new BancoDbMapping());
            modelBuilder.ApplyConfiguration(new CentroCustoDbMapping());
            modelBuilder.ApplyConfiguration(new FormaRecebimentoDbMapping());
            modelBuilder.ApplyConfiguration(new MeioPagamentoDbMapping());
            modelBuilder.ApplyConfiguration(new PlanoContaDbMapping());
            modelBuilder.ApplyConfiguration(new SubContaDbMapping());

            modelBuilder.ApplyConfiguration(new BoletoDbMapping());
            modelBuilder.ApplyConfiguration(new BolsaDbMapping());
            modelBuilder.ApplyConfiguration(new CaixaDbMapping());
            //modelBuilder.ApplyConfiguration(new FornecedorDbMapping());
            //modelBuilder.ApplyConfiguration(new InformacaoDebitoDbMapping());
            modelBuilder.ApplyConfiguration(new ReparceladoDbMapping());
        }
    }
}
