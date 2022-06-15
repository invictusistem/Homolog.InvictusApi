using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Domain.Financeiro.Fornecedores.Interfaces
{
    public interface IFornecedorRepo : IDisposable
    {
        //Task SaveFornecedor(Fornecedor fornecedor);
        //Task Edit(Fornecedor fornecedor);
        void Commit();
    }
}
