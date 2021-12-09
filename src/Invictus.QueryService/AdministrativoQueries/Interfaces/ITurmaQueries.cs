using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface ITurmaQueries
    {
        Task<int> CountTurmas(Guid unidadeId);
        Task<IEnumerable<TurmaMateriasDto>> GetMateriasFromPacotesMaterias(Guid pacoteId);
        Task<IEnumerable<TurmaMateriasDto>> GetTurmaMateriasFromProfessorId(Guid professorId, Guid turmaId);
        Task<IEnumerable<TurmaViewModel>> GetTurmas();
        Task<IEnumerable<TurmaInfoCompleta>> GetTurmaInfo(Guid turmaId);
        Task<IEnumerable<TurmaViewModel>> GetTurmasByType(Guid typepacote);
        Task<TurmaMatriculaViewModel> GetTurmasById(Guid turmaId);
        Task<TurmaDto> GetTurma(Guid turmaId);
        Task<IEnumerable<TurmaMateriasDto>> GetMateriasDaTurma(Guid turmaId);
        Task<IEnumerable<Guid>> GetTypePacotesTurmasMatriculadas(Guid alunoId);
        Task<IEnumerable<ProfessorTurmaView>> GetProfessoresDaTurma(Guid turmaId);
        Task<List<MateriaView>> GetMateriasLiberadas(Guid turmaId, Guid professorId);
        Task<TurmaMateriasDto> GetTurmaMateria(Guid turmaMateriaId);
        Task<TurmaProfessoresDto> GetTurmaProfessor(Guid professorId, Guid turmaId);
        Task<IEnumerable<TurmaViewModel>> GetTurmasPedagViewModel();
    }
}
