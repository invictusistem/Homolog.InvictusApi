using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IPedagModelsQueries
    {
        Task<IEnumerable<EstagioDto>> GetListEstagios();
    }
}
