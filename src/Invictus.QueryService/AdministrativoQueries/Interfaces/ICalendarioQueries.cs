using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface ICalendarioQueries
    {
        Task<IEnumerable<TurmaCalendarioViewModel>> GetCalendarioByTurmaId(Guid turmaId);
        Task<PaginatedItemsViewModel<TurmaCalendarioViewModel>> GetCalendarioPaginatedByTurmaId(Guid turmaId, int itemsPerPage, int currentPage, string paramsJson);
        //Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradoresByUnidadeId(int itemsPerPage, int currentPage, string paramsJson);
        Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByProfessorIdAndUnidadeId(Guid unidadeId, Guid professorId);
        Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByProfessorIdAndMateriaId(Guid materiaId, Guid professorId);
        Task<IEnumerable<TurmaCalendarioViwModel>> GetFutureCalendarsByTurmaIdAndMateriaId(Guid materiaId, Guid turmaId);
        Task<TurmaCalendarioViewModel> GetCalendarioViewModelById(Guid calendarioId);
        Task<TurmaCalendarioViwModel> GetCalendarioById(Guid calendarioId);
        Task<IEnumerable<TurmaCalendarioViwModel>> GetCalendarioByProfessorId(Guid professorId);
        Task<AulaViewModel> GetAulaViewModel(Guid calendarioId);
    }
}
