using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface ITypePacoteQueries
    {
        Task<IEnumerable<TypePacoteDto>> GetTypePacotes();
        Task<TypePacoteDto> GetTypePacote(Guid typePacoteId);
    }
}
