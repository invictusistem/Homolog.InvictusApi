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
        Task<PaginatedItemsViewModel<FornecedorDto>> GetFornecedores(int itemsPerPage, int currentPage, string paramsJson);
        Task<IEnumerable<FornecedorDto>> GetAllFornecedores();
        Task<FornecedorDto>  GetFornecedor(Guid fornecedorId);
    }
}
