using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IUnidadeQueries
    {
        Task<IEnumerable<UnidadeDto>> GetUnidades();
        Task<UnidadeDto> GetUnidadeDoUsuario();
        Task<UnidadeDto> GetUnidadeBySigla(string sigla);
        Task<IEnumerable<SalaDto>> GetSalas(Guid unidadeId);
        Task<IEnumerable<SalaDto>> GetSalasByUserUnidade();
        Task<SalaDto> GetSala(Guid salaId);
        Task<UnidadeDto> GetUnidadeById(Guid id);
        Task<IEnumerable<UnidadeDto>> GetUnidadesDonatarias(string unidadeSigla);
        Task<int> CountSalaUnidade(Guid unidadeId);

    }
}
