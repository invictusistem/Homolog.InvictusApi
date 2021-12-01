using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface IPedagDocsQueries
    {
        Task<IEnumerable<AlunoDocumentoDto>> GetDocsMatriculaViewModel(Guid matriculaId);
        Task<AlunoDocumentoDto> GetDocumentById(Guid docId);
    }
}
