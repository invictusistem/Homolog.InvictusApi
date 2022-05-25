using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IMateriaTemplateQueries
    {
        Task<PaginatedItemsViewModel<MateriaTemplateDto>> GetMateriasTemplateList(int itemsPerPage, int currentPage);
        Task<IEnumerable<MateriaTemplateDto>> GetAllMaterias();
        Task<IEnumerable<MateriaTemplateDto>> GetMateriaByTypePacoteId(Guid typePacoteId);
        Task<IEnumerable<MateriaTemplateDto>> GetMateriasByTypePacoteLiberadoParaOProfessor(Guid typePacoteId, Guid professorId);
        Task<MateriaTemplateDto> GetMateriaTemplate(Guid materiaId);
        Task<IEnumerable<MateriaTemplateDto>> GetMateriasByListIds(List<Guid> listGuidMaterias);
        // Task<PaginatedItemsViewModel<MateriaTemplateDto>> GetMateriaTemplate(Guid materiaId, int itemsPerPage, int currentPage, string paramsJson);

    }
}
