using Invictus.Dtos.AdmDtos;
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
       Task UpdateFornecedor(PessoaDto fornecedor);
       Task CreateFornecedor(PessoaDto fornecedor);
    }
}
