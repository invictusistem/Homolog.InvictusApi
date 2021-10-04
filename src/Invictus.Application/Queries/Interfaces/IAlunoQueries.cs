using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IAlunoQueries
    {
        Task<AlunoDto> GetAluno(int alunoId);
        Task<IEnumerable<DocumentoAlunoDto>> GetDocsAluno(int alunoId);
        
    }
}
