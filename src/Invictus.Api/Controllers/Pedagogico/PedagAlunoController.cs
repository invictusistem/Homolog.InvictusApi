using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/aluno")]
    [Authorize]
    [ApiController]
    public class PedagAlunoController : ControllerBase
    {
        private readonly IPedagMatriculaQueries _pedagMatriculaQueries;
        private readonly ITurmaPedagQueries _pedagTurmaQueries;
        private readonly IPedagDocsQueries _pedagDocsQueries;
        private readonly IPedagogicoApplication _pedagApplication;

        public PedagAlunoController(IPedagMatriculaQueries pedagMatriculaQueries, ITurmaPedagQueries pedagTurmaQueries, IPedagDocsQueries pedagDocsQueries,
            IPedagogicoApplication pedagApplication)
        {
            _pedagMatriculaQueries = pedagMatriculaQueries;
            _pedagTurmaQueries = pedagTurmaQueries;
            _pedagDocsQueries = pedagDocsQueries;
            _pedagApplication = pedagApplication;
        }

        [HttpGet]
        [Route("{matriculaId}")]
        public async Task<IActionResult> GetInformacoesAlunoViewModel(Guid matriculaId)
        {
            // cadastro aluno baseado na matricula

            var turma = await _pedagTurmaQueries.GetTurmaByMatriculaId(matriculaId);

            var aluno = await _pedagMatriculaQueries.GetAlunoByMatriculaId(matriculaId);

            var respFin = await _pedagMatriculaQueries.GetRespFinanceiroByMatriculaId(matriculaId);

            var respMenor = await _pedagMatriculaQueries.GetRespMenorByMatriculaId(matriculaId);

            var anotacoes = await _pedagMatriculaQueries.GetAnotacoesMatricula(matriculaId);

            var docs = await _pedagDocsQueries.GetDocsMatriculaViewModel(matriculaId);

            return Ok(new { aluno = aluno, respFin = respFin, respMenor = respMenor, anotacoes = anotacoes, turma = turma, docs = docs });
        }

        

        [HttpGet]
        [Route("responsavel/{respId}")]
        public async Task<IActionResult> GetResponsavel(Guid respId)
        {
            var resp = await _pedagMatriculaQueries.GetResponsavel(respId);

            return Ok(new { resp = resp });
        }

        [HttpGet]
        [Route("responsavel-aluno/{respId}")]
        public async Task<IActionResult> GetResponsavelById(Guid respId)
        {
            var resp = await _pedagMatriculaQueries.GetResponsavelById(respId);

            return Ok(new { resp = resp });
        }

        [HttpGet]
        [Route("aluno/{matriculaId}")]
        public async Task<IActionResult> GetAluno(Guid matriculaId)
        {
            var aluno = await _pedagMatriculaQueries.GetAlunoByMatriculaId(matriculaId);

            return Ok(new { aluno = aluno, });
        }

        [HttpGet]
        [Route("alunos/{turmaId}")]
        public async Task<IActionResult> GetAlunos(Guid turmaId)
        {
            var alunos = await _pedagTurmaQueries.GetAlunosDaTurma(turmaId);

            return Ok(new { alunos = alunos });
        }

        [HttpGet]
        [Route("nota/{matriculaId}")]
        public async Task<IActionResult> GetNotas(Guid matriculaId)
        {
            var notas = await _pedagTurmaQueries.GetNotaAluno(matriculaId);

            return Ok(new { notas = notas });
        }


        [HttpPut]
        [Route("responsavel")]
        public async Task<IActionResult> EditResponsavel([FromBody] ResponsavelDto responsavel)
        {
            await _pedagApplication.EditResponsavel(responsavel);

            return Ok();
        }




    }
}
