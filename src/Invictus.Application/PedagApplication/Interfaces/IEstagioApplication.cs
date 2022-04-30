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
        Task DeleteTypeEstagio(Guid typeEstagio);
        Task CreateTypeEstagio(TypeEstagioDto typeEstagio);
        Task EditTypeEstagio(TypeEstagioDto typeEstagio);
        Task LiberarMatricula(LiberarEstagioCommand command);
        Task AprovarDocumento(Guid documentoId, bool aprovar);

    }
}
