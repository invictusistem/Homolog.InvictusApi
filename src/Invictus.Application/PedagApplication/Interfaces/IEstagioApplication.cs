using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication.Interfaces
{
    public interface IEstagioApplication
    {
        Task CreateEstagio(EstagioDto estagioDto);
        Task EditEstagio(EstagioDto estagioDto);

    }
}
