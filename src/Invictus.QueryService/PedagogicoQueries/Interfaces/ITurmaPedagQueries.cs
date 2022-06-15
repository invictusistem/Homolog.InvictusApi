using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invictus.QueryService.PedagogicoQueries.Interfaces
{
    public interface ITurmaPedagQueries
    {
        Task<IEnumerable<PessoaDto>> GetAlunosDaTurma(Guid turmaId);
        Task<TurmaDto> GetTurmaByMatriculaId(Guid matriculaId);
        Task<IEnumerable<PessoaDto>> GetProfessoresDaTurma(Guid turmaId);
        Task<IEnumerable<TurmaNotasViewModel>> GetNotasFromTurma(Guid turmaId, Guid materiaId);
        Task<IEnumerable<TurmaNotasViewModel>> GetNotaAluno(Guid matriculaId);
        Task<ListPresencaViewModel> GetInfoDiaPresencaLista(Guid turmaId, Guid calendarioId);
        Task<string> VerificarSeAlunoDisponivelParaTransfInterna(string cpf);

    }
}
