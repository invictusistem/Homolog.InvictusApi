using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication.Interfaces
{
    public interface ICalendarioApp
    {
        Task<TurmaCalendarioViewModel> EditCalendario(AulaViewModel aula, Guid calendarioId);
    }
}
