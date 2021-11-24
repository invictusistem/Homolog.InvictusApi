using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IContratoQueries
    {
        Task<IEnumerable<ContratoDto>> GetContratosViewModel();
        Task<ContratoDto> GetContratoById(Guid contratoId);
        Task<IEnumerable<ContratoDto>> GetContratoByTypePacote(Guid typePacoteId);
        Task<int> CountContratos();
        Task<ContratoDto> GetContratoCompletoById(Guid contratoId);
    }
}
