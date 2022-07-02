using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface IPedagReqQueries
    {
        Task<IEnumerable<CategoriaDto>> GetAllCategorias();
        Task<IEnumerable<TipoDto>> GetTiposByCategoriaId(Guid categoriaId);
        Task<TipoDto> GetTipoById(Guid tipoId);
        Task<CategoriaDto> GetCategoriaById(Guid id);

    }
}
