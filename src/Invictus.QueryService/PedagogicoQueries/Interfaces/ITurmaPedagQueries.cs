using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface ITurmaPedagQueries
    {
        Task<IEnumerable<AlunoDto>> GetAlunosDaTurma(Guid turmaId);
        Task<TurmaDto> GetTurmaByMatriculaId(Guid matriculaId);
        Task<IEnumerable<ProfessorDto>> GetProfessoresDaTurma(Guid turmaId);
        Task<IEnumerable<TurmaNotasViewModel>> GetNotasFromTurma(Guid turmaId, Guid materiaId);
    }
}
