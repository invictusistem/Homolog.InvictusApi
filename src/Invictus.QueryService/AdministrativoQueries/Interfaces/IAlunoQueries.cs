using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IAlunoQueries
    {
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosView(int itemsPerPage, int currentPage, string paramsJson);
        Task<IEnumerable<AlunoDto>> FindAlunoByCPForEmailorRG(string cpf, string rg, string email);
        Task<IEnumerable<AlunoDto>> SearchPerCPF(string cpf);
        Task<DateTime> GetIdadeAluno(Guid alunoId);
        Task<AlunoDto> GetAlunoById(Guid alunoId);
    }
}
