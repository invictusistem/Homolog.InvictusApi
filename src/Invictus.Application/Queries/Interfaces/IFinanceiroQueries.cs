using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Financeiro;
using Invictus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IFinanceiroQueries
    {
        Task<IEnumerable<string>> GetUsuarios(QueryDto param/*, int itemsPerPage, int currentPage*/);
        Task<PaginatedItemsViewModel<AlunoDto>> GetAlunoFin(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId); // GetAlunoFin(int itemsPerPage, int currentPage, ParametrosDTO param, int unidadeId)
        Task<IEnumerable<DebitoDto>> GetDebitoAlunos(int alunoid, int turmaId);
        Task<IEnumerable<DebitoDto>> GetBalancoCursos(string start, string end, int unidadeId);
        Task<IEnumerable<BalancoProdutosViewModel>> GetBalancoProdutos(string start, string end, int unidadeId);
    }
}
