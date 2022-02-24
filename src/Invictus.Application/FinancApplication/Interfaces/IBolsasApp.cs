using Invictus.Dtos.Financeiro;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication.Interfaces
{
    public interface IBolsasApp
    {
        Task<string> SaveBolsa(BolsaDto bolsa);
        Task EditBolsa(BolsaDto bolsa);

    }
}
