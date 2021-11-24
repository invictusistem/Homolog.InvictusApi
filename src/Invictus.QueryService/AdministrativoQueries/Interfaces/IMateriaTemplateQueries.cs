using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IMateriaTemplateQueries
    {
        Task<IEnumerable<MateriaTemplateDto>> GetMateriasTemplateList();
        Task<IEnumerable<MateriaTemplateDto>> GetMateriaByTypePacoteId(Guid typePacoteId);
        Task<MateriaTemplateDto> GetMateriaTemplate(Guid materiaId);

    }
}
