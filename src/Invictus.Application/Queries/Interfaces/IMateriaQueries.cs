using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IMateriaQueries
    {
        Task<IEnumerable<ProfessoresMateriaDto>> GetMaterias(int turmaId, int profId);
    }
}
