using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface ITemplateQueries
    {      

        // Plano Pagamento
        Task<PaginatedItemsViewModel<PlanoPagamentoDto>> GetListPlanoPagamentoTemplate(int itemsPerPage, int currentPage);
        Task<PlanoPagamentoDto> GetPagamentoTemplateById(Guid materiaTemplateId);
    }
}
