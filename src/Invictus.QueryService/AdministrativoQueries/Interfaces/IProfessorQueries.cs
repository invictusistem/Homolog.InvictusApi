using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IProfessorQueries 
    {
        Task<PaginatedItemsViewModel<PessoaDto>> GetProfessores(int itemsPerPage,int currentPage, string paramsJson);
        Task<PaginatedItemsViewModel<PessoaDto>> GetProfessoresV2(int itemsPerPage, int currentPage, string paramsJson);
        Task<PessoaDto> GetProfessorById(Guid professorId);
        Task<MateriaHabilitadaViewModel> GetProfessorMateria(Guid professorMateriaId);
        Task<ProfessorRelatorioViewModel> GetReportHoursTeacher(DateTime rangeIni, DateTime rangeFinal, Guid teacherId);
        Task<IEnumerable<MateriaHabilitadaViewModel>> GetProfessoresMaterias(Guid professorId);
        Task<IEnumerable<UnidadeDto>> GetProfessoresUnidadesDisponiveis(Guid professorId);
        Task<IEnumerable<DisponibilidadeView>> GetProfessorDisponibilidade(Guid professorId);
        Task<IEnumerable<PessoaDto>> GetProfessoresDisponiveis(Guid turmaId);
        Task<IEnumerable<PessoaDto>> GetProfessoresDisponiveisByFilter(string diaDaSemana, Guid unidadeId, Guid materiaId);
        Task<IEnumerable<ProfessorCalendarioViewModel>> GetProfessorCalendario(Guid professorId);
        Task<string> GetEmailDoProfessorById(Guid professorId);
    }
}
