using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface ICursoQueries
    {
        Task<IEnumerable<TurmaViewModel>> GetCursos(string unidade);
        Task<IEnumerable<TurmaViewModel>> GetCursoById(int cursoId);
        Task<IEnumerable<TurmaViewModel>> GetCursosAndamento(string curso, int[] typePacoteIds,int unidadeId);
        Task<List<AlunoDto>> GetAlunosDaTurma(int turmaId);
        Task<IEnumerable<TurmaViewModel>> GetCursosUnidade();
        Task<int> GetQuantidadeTurma(int unidadeId);
    }
}

