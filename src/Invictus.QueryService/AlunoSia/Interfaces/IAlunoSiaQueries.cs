using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AlunoSia.Interfaces
{
    public interface IAlunoSiaQueries
    {
        Task<IEnumerable<DocumentoEstagioDto>> GetDocumentosEstagio(Guid matriculaId);
        Task<IEnumerable<TypeEstagioDto>> GetEstagiosTiposLiberadosDoAluno(Guid matriculaId);
        Task<IEnumerable<EstagioDto>> GetEstagiosLiberados(Guid tipoEstagioId);
        Task<EstagioDto> GetEstagio(Guid estagioId);
    }
}
