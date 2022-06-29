using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface IPedagMatriculaQueries
    {
        Task<PessoaDto> GetAlunoByMatriculaId(Guid matriculaId);
        Task<MatriculaDto> GetMatriculaById(Guid matriculaId);
        Task<ResponsavelDto> GetRespFinanceiroByMatriculaId(Guid matriculaId);
        Task<ResponsavelDto> GetRespMenorByMatriculaId(Guid matriculaId);
        Task<IEnumerable<AnotacaoDto>> GetAnotacoesMatricula(Guid matriculaId);
        Task<ResponsavelDto> GetResponsavel(Guid matriculaId);
        Task<ResponsavelDto> GetResponsavelById(Guid id);
        Task<IEnumerable<PessoaDto>> GetAlunosIndicacao();
        Task<IEnumerable<MatriculaViewModel>> GetRelatorioMatriculas(string param);
        Task<IEnumerable<MatriculaViewModel>> GetMatriculadosFromUnidade();
        Task<MatriculaViewModel> GetMatriculaByNumeroMatricula(string numeroMatricula);

    }
}
