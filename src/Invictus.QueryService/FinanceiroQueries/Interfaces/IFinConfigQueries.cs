using Invictus.Dtos.Financeiro.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IFinConfigQueries
    {
        Task<IEnumerable<BancoDto>> GetAllBancos();
        Task<IEnumerable<CentroCustoDto>> GetAllCentroCusto();
        Task<IEnumerable<MeioPagamentoDto>> GetAllMeiosPagamento();
        Task<IEnumerable<PlanoContaDto>> GetAllPlanos();
        Task<IEnumerable<SubContaDto>> GetAllSubContas();

        Task<BancoDto> GetAllBancoById(Guid id);
        Task<CentroCustoDto> GetAllCentroCustoById(Guid id);
        Task<MeioPagamentoDto> GetAllMeiosPagamentoById(Guid id);
        Task<PlanoContaDto> GetAllPlanosById(Guid id);
        Task<SubContaDto> GetAllSubContasById(Guid id);
        Task<IEnumerable<SubContaDto>> GetAllSubContasByPlanoId(Guid planoId);
    }
}
