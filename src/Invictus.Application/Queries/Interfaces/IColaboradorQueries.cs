using Invictus.Application.Dtos;
using Invictus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IColaboradorQueries
    {
        Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradores(int itemsPerPage, int currentPage, ParametrosDTO parametros, int unidadeId);
        //Task<PaginatedItemsViewModel<ColaboradorDto>> GetProfessores(string unidade);
        Task<IEnumerable<ColaboradorDto>> GetProfessores(string unidade, int turmaId);
        Task<IEnumerable<ProfessoresDto>> GetProfessoresByTurmaId(int turmaId);
        Task<PaginatedItemsViewModel<ColaboradorDto>> GetUsuarios(QueryDto param, int itemsPerPage, int currentPage);
        Task<ColaboradorDto> SearhColaborador(string email);
    }
}
