using Invictus.Dtos.Financeiro.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication.Interfaces
{
    public interface IFinancConfigApp
    {
        Task SaveBanco(BancoDto banco);
        Task SaveCentroDeCusto(CentroCustoDto centroCusto);
        Task SaveMeioDePagamento(MeioPagamentoDto meioPgm);
        Task SavePlanoDeConta(PlanoContaDto plano);
        Task SaveSubConta(SubContaDto subConta);
        Task SaveFormRecebimento(FormaRecebimentoDto newFormaRecebimento);


        Task EditBanco(BancoDto banco);
        Task EditCentroDeCusto(CentroCustoDto centroCusto);
        Task EditMeioDePagamento(MeioPagamentoDto meioPgm);
        Task EditPlanoDeConta(PlanoContaDto plano);
        Task EditSubConta(SubContaDto subConta);
        Task EditFormaRecebimento(FormaRecebimentoDto editedFormaRecebimento);


        Task DeleteBanco(Guid bancoId);
        Task DeleteCentroDeCusto(Guid centroCustoId);
        Task DeleteMeioDePagamento(Guid meioPgmId);
        Task DeletePlanoDeConta(Guid planoId);
        Task DeleteSubConta(Guid subContaId);
        Task DeleteFormaRecebimento(Guid formarecebimentoId);
    }
}
