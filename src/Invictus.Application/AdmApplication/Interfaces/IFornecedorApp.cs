using Invictus.Dtos.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IFornecedorApp
    {
        Task UpdateFornecedor(FornecedorDto fornecedor);
        Task CreateFornecedor(FornecedorDto fornecedor);
    }
}
