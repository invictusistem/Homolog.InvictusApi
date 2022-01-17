using Invictus.Dtos.PedagDto;
using System.Threading.Tasks;

namespace Invictus.Application.PedagApplication.Interfaces
{
    public interface IPedagogicoApplication
    {
        Task EditResponsavel(ResponsavelDto responsavel);
    }
}
