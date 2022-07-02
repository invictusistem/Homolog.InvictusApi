using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication.Interfaces
{
    public interface IPedagReqService
    {
        Task EditCategoria(CategoriaDto categoria);
        Task EditTipo(TipoDto tipo);
        Task SaveCategoria(CategoriaDto categoria);
        Task SaveTipo(TipoDto tipo);

    }
}
