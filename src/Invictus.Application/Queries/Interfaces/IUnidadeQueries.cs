using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IUnidadeQueries
    {
        Task<List<string>> GetMateriasDoCurso(int moduloId);
        Task<List<MateriaDto>> GetMaterias(int moduloId);
        Task<IEnumerable<ProdutoViewModel>> GetProdutosViewModel();
        Task<PaginatedItemsViewModel<ProfessorDto>> GetProfessores(int itemsPerPage, int currentPage, ParametrosDTO parametros, int unidadeId);
    }
}
