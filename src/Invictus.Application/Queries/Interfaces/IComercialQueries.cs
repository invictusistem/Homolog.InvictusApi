using Invictus.Application.Dtos;
using Invictus.Domain.Administrativo.AlunoAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IComercialQueries
    {
        Task<IEnumerable<ResultMetricaDto>> GetAlunosMatriculados();
    }
}
