using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Configuracoes.Interfaces
{
    public interface IFinanceiroConfigRepo : IDisposable
    {
        Task AddBanco(Banco banco);
        Task AddCentroCusto(CentroCusto banco);
        Task AddFormaRecebimento(FormaRecebimento banco);
        Task AddMeioPagamento(MeioPagamento banco);
        Task AddPlanoConta(PlanoConta banco);
        Task AddSubConta(SubConta banco);

        void Commit();

    }
}
