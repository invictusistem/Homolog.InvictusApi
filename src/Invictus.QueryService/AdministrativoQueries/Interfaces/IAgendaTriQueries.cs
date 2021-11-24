using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IAgendaTriQueries
    {
        Task<IEnumerable<AgendaTrimestreDto>> GetAgendas(Guid unidadeId, int ano);
        Task<AgendaTrimestreDto> GetAgenda(Guid agendaId);

    }
}
