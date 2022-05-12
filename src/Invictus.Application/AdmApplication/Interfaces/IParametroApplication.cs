using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface IParametroApplication
    {
        Task SaveCargo(string key, ParametroValueDto paramValue);
        Task EditCargo(ParametroValueDto paramValue);
        Task RemoeValueById(Guid paramId);
    }
}
