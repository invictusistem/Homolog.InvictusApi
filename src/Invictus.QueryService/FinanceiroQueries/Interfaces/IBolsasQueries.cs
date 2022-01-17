using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IBolsasQueries
    {
        Task<IEnumerable<BolsaDto>> GetBolsas(Guid typePacoteId);
        Task<string> GetSenha(Guid bolsaId);
        Task<BolsaDto> GetBolsa(string senha);
    }
}
