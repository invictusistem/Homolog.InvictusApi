﻿using Invictus.QueryService.PedagogicoQueries.Interfaces;
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

        public PedagAlunoController(IPedagMatriculaQueries pedagMatriculaQueries, ITurmaPedagQueries pedagTurmaQueries, IPedagDocsQueries pedagDocsQueries)
        {
            _pedagMatriculaQueries = pedagMatriculaQueries;
            _pedagTurmaQueries = pedagTurmaQueries;
            _pedagDocsQueries = pedagDocsQueries;
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
        [Route("aluno/{matriculaId}")]
        public async Task<IActionResult> GetAluno(Guid matriculaId)
        {

            var aluno = await _pedagMatriculaQueries.GetAlunoByMatriculaId(matriculaId);


            return Ok(new { aluno = aluno, });
        }

    }
}
