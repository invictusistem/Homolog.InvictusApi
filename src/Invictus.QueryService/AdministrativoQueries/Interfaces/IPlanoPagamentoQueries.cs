using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IPlanoPagamentoQueries
    {
        Task<IEnumerable<PlanoPagamentoDto>> GetPlanosByTypePacote(Guid typePacoteId);
        Task<PlanoPagamentoDto> GetPlanoById(Guid planoId);
    }
}
