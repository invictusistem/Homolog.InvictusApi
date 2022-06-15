using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Financeiro;
using Invictus.Dtos.Financeiro.Configuracoes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IFinConfigQueries
    {
        Task<IEnumerable<BancoDto>> GetAllBancos();
        Task<IEnumerable<BancoDto>> GetAllBancosAtivosFromUnidade();
        Task<IEnumerable<MeioPagamentoDto>> GetAllMeiosPagamento();
        Task<IEnumerable<PlanoContaDto>> GetAllPlanos();
        Task<IEnumerable<CentroCustoDto>> GetAllCentroCusto();
        Task<IEnumerable<FormaRecebimentoDto>> GetAllFormasRecebimentos();
        Task<IEnumerable<SubContaDto>> GetAllSubContas();
        Task<IEnumerable<SubContaDto>> GetAllSubContasAtivas();
        Task<IEnumerable<SubContaDto>> GetAllSubContasAtivasDebitos();
        Task<IEnumerable<PessoaDto>> GetFornecedoresForCreateFormaRecebimento();



        Task<BancoDto> GetBancoById(Guid id);
        Task<CentroCustoDto> GetCentroCustoById(Guid id);
        Task<MeioPagamentoDto> GetMeiosPagamentoById(Guid id);
        Task<PlanoContaDto> GetPlanosById(Guid id);
        Task<FormaRecebimentoDto> GetFormaRecebimentoById(Guid id);
        Task<SubContaDto> GetSubContasById(Guid id);

        Task<IEnumerable<SubContaDto>> GetSubContasByPlanoId(Guid id);
    }
}
