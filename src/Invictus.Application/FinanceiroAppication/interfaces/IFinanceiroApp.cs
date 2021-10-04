using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Domain.Financeiro.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinanceiroAppication.interfaces
{
    public interface IFinanceiroApp
    {
        void addProduct(Produto newProduct);
        Task<IEnumerable<ProdutoDto>> SearchProduct(string nome, int unidadeId);
        Task<IEnumerable<FornecedorDto>> SearchFornecedor(QueryDto query);


    }
}
