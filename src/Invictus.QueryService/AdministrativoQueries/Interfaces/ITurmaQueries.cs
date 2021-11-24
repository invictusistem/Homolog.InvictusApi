using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface ITurmaQueries
    {
        Task<int> CountTurmas(Guid unidadeId);
        Task<IEnumerable<TurmaMateriasDto>> GetMateriasFromPacotesMaterias(Guid pacoteId);
        Task<IEnumerable<TurmaViewModel>> GetTurmas();
        Task<IEnumerable<TurmaInfoCompleta>> GetTurmaInfo(Guid turmaId);
        Task<IEnumerable<TurmaViewModel>> GetTurmasByType(Guid typepacote);
        Task<TurmaMatriculaViewModel> GetTurmasById(Guid turmaId);
        Task<TurmaDto> GetTurma(Guid turmaId);
        Task<IEnumerable<TurmaMateriasDto>> GetMateriasDaTurma(Guid turmaId);
        Task<IEnumerable<Guid>> GetTypePacotesTurmasMatriculadas(Guid alunoId);
    }
}
