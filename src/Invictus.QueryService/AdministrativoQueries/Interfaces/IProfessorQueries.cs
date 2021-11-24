using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IProfessorQueries 
    {
        Task<PaginatedItemsViewModel<ProfessorDto>> GetProfessores(int itemsPerPage,int currentPage, string paramsJson);
        Task<ProfessorDto> GetProfessorById(Guid professorId);
    }
}
