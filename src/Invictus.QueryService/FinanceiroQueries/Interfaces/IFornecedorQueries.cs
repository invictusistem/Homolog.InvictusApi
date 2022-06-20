using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.FinanceiroQueries.Interfaces
{
    public interface IFornecedorQueries
    {
        Task<PaginatedItemsViewModel<PessoaDto>> GetFornecedores(int itemsPerPage, int currentPage, string paramsJson);
        Task<List<PessoaDto>> GetAllFornecedores(Guid? pessoaId);
        Task<IEnumerable<PessoaDto>> GetAllColaboradoresAndProfessores();
        Task<PessoaDto> GetFornecedor(Guid fornecedorId);
    }
}
