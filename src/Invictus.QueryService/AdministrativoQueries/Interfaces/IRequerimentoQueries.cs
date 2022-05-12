using Invictus.Dtos.AdmDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.AdministrativoQueries.Interfaces
{
    public interface IRequerimentoQueries
    {
        Task<TipoRequerimentoDto> GetTipoRequerimentoById(Guid tipoId);
        Task<IEnumerable<TipoRequerimentoDto>> GetTiposRequerimentos();
        Task<IEnumerable<RequerimentoDto>> GetRequerimentosByMatriculaId(Guid matriculaId);
        Task<IEnumerable<RequerimentoDto>> GetRequerimentosByUnidadeId();
    }
}
