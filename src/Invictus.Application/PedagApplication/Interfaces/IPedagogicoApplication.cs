using Invictus.Dtos.PedagDto;
using System.Threading.Tasks;
using System;

namespace Invictus.Application.PedagApplication.Interfaces
{
    public interface IPedagogicoApplication
    {
        Task EditResponsavel(ResponsavelDto responsavel);
        Task IniciarAula(Guid calendarioId);
    }
}
