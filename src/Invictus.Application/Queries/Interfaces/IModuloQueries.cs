using Invictus.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.Application.Queries.Interfaces
{
    public interface IModuloQueries
    {
        Task<IEnumerable<MateriaDto>> GetMateriasModulos(int moduloId);
        Task<IEnumerable<ModuloDto>> GetModulos(string unidade);
        Task<IEnumerable<ModuloDto>> GetModulosCreateTurma(int unidadeId);
    }
}
