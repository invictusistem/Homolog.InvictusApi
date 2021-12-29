using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IFinanceiroQueries
    {
        Task<IEnumerable<BoletoDto>> GetDebitoAlunos(Guid matriculaId);
    }
}
