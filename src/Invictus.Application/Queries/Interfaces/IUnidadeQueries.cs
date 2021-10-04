using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IUnidadeQueries
    {
        Task<List<string>> GetMateriasDoCurso(int moduloId);
        Task<List<MateriaDto>> GetMaterias(int moduloId);
    }
}
