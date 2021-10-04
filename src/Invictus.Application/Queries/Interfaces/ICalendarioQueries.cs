using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface ICalendarioQueries
    {
        Task<IEnumerable<CalendarioDto>> GetDatas(DateTime inicio, DateTime fim);
        Task<IEnumerable<TurmaCalendarioViewModel>> GetCalendarioByTurmaId(int turmaId);
    }
}
