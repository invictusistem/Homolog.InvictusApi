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

        Task EditBanco(Banco banco);
        Task EditCentroCusto(CentroCusto centroCusto);
        Task EditFormaRecebimento(FormaRecebimento formareceb);
        Task EditMeioPagamento(MeioPagamento meioPgm);
        Task EditPlanoConta(PlanoConta plano);
        Task EditSubConta(SubConta subconta);


        Task DeleteBanco(Guid bancoId);
        Task DeleteCentroCusto(Guid centroCustoId);
        Task DeleteFormaRecebimento(Guid formarecebId);
        Task DeleteMeioPagamento(Guid meioPgmId);
        Task DeletePlanoConta(Guid planoId);
        Task DeleteSubConta(Guid subcontaId);

        void Commit();

    }
}
