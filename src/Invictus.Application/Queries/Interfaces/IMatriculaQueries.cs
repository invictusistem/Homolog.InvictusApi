using Invictus.Application.Dtos;
using Invictus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IMatriculaQueries 
    {
        Task<IEnumerable<string>> SearchEmail(string email);
        Task<IEnumerable<string>> SearchCPF(string cpf);
        Task<IEnumerable<AlunoDto>> BuscarLeads(string email = null, string cpf = null, string nome = null);

        Task<PaginatedItemsViewModel<AlunoDto>> BuscaAlunos(int itemsPerPage, int currentPage, ParametrosDTO parametros, int unidadeId);
        //Task<List<AlunoDto>> BuscaAlunos(string email = null, string cpf = null, string nome = null);
    }
}
