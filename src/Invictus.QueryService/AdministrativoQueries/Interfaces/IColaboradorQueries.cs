using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IColaboradorQueries
    {
        Task<PaginatedItemsViewModel<ColaboradorDto>> GetColaboradoresByUnidadeId(int itemsPerPage, int currentPage, string paramsJson);
        Task<ColaboradorDto> GetColaboradoresByEmail(string email);
        Task<ColaboradorDto> GetColaboradoresById(Guid colaboradorId);
    }
}
