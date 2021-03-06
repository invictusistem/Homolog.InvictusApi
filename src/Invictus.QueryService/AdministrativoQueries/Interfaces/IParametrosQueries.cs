using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IParametrosQueries
    {
        Task<ParametrosKeyDto> GetParamKey(string value);
        Task<IEnumerable<ParametroValueDto>> GetParamValue(string key);
        Task<ParametroValueDto> GetParamKeyById(Guid valueId);
    }
}
