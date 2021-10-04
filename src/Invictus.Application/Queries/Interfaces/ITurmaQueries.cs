using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface ITurmaQueries
    {
        Task<string> GetPrevisaoAtual(int turmaId);
        Task<IEnumerable<AgendasProvasDto>> GetAgendas(int turmaId, int avaliacao);
        Task<IEnumerable<TurmaViewModel>> GetTurmasComVagas(int unidadeId);
        Task<IEnumerable<TurmaViewModel>> GetTurmasMatriculadosOutraUnidade(int alunoId, int unidadeId);
    }
    
}
