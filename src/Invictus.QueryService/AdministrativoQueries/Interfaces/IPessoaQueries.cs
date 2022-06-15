using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IPessoaQueries
    {
        Task<PessoaDto> GetFornecedorById(Guid fornecedorId);
    }
}
