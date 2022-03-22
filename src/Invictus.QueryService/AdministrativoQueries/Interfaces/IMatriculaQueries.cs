using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IMatriculaQueries
    {
        Task<IEnumerable<TypePacoteDto>> GetTypesLiberadorParaMatricula(Guid alunoId);
        Task<int> TotalMatriculados();
        Task<string> GetInfoMatriculasByCPF(string cpf);
    }
}
