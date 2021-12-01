using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IPacoteQueries
    {
        Task<IEnumerable<PacoteDto>> GetPacotes(Guid typePacoteId, Guid unidadeId);
        Task<IEnumerable<PacoteMateriaDto>> GetMateriasPacote(Guid pacoteId);
        Task<IEnumerable<PacoteDto>> GetPacotesByUserUnidade(Guid typePacoteId);
        Task<IEnumerable<DocumentacaoExigidaDto>> GetDocsByPacoteId(Guid pacoteId);
        Task<PacoteDto> GetPacoteById(Guid typePacoteId);

        Task<PacoteDtoTeste> GetPacoteByIdTeste(Guid pacoteId);
    }
}
