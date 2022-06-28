using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IProdutoQueries
    {
        Task<ProdutoDto> GetProdutobyId(Guid produtoId);
        Task<IEnumerable<ProdutoDto>> BuscaProduto(string nome);
        Task<IEnumerable<string>> SearchProductByName(string nome);
        Task<IEnumerable<ProdutoDto>> GetProdutos();
        Task<int> ProdutosCount();
    }
}
