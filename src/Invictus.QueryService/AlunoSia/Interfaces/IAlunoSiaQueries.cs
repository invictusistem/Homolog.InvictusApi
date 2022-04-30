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
    }
}
