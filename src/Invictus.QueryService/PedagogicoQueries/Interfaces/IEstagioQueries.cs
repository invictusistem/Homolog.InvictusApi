using Invictus.Dtos.AdmDtos.Utils;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface IEstagioQueries
    {
        Task<IEnumerable<EstagioDto>> GetEstagios();
        Task<EstagioDto> GetEstagioById(Guid estagioId);
        Task<PaginatedItemsViewModel<ViewMatriculadosDto>> GetMatriculadosView(int itemsPerPage, int currentPage, string paramsJson);
    }
}
