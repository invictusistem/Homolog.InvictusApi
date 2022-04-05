using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/matricula")]
    [Authorize]
    [ApiController]
    public class MatriculaController : ControllerBase
    {

        private readonly IMatriculaQueries _matriculaQueries;
        private readonly IMatriculaApplication _matriculaApplication;
        private readonly IPedagMatriculaQueries _pedagMatriculaQueries;
        private readonly IUtils _utils;
        public MatriculaController(IMatriculaQueries matriculaQueries, IMatriculaApplication matriculaApplication, IPedagMatriculaQueries pedagMatriculaQueries,
            IUtils utils)
        {
            _matriculaQueries = matriculaQueries;
            _matriculaApplication = matriculaApplication;
            _pedagMatriculaQueries = pedagMatriculaQueries;
            _utils = utils;
        }

        [HttpGet]
        [Route("{alunoId}")]
        public async Task<IActionResult> GetTypeLiberadosParaMatricula(Guid alunoId)
        {
            var types = await _matriculaQueries.GetTypesLiberadorParaMatricula(alunoId);

            if (types.Count() == 0) return NotFound();

            return Ok(new { types = types });
        }

        [HttpGet]
        [Route("anotacao/{matriculaId}")]
        public async Task<IActionResult> GetInformacoesAluno(Guid matriculaId)
        {
            // cadastro aluno baseado na matricula

            var anotacoes = await _pedagMatriculaQueries.GetAnotacoesMatricula(matriculaId);
          

            return Ok(new { anotacoes = anotacoes });
        }

        [HttpGet]
        [Route("aluno-indicacao")]
        public async Task<IActionResult> GetAlunosIndicacao()
        {
            // cadastro aluno baseado na matricula

            var alunos = await _pedagMatriculaQueries.GetAlunosIndicacao();



            return Ok(new { alunos = alunos });
        }

        [HttpGet]
        [Route("relatorio")]
        public async Task<IActionResult> GetRelatorio([FromQuery] string paramJson)
        {            
            var matriculas = await _pedagMatriculaQueries.GetRelatorioMatriculas(paramJson);

            if (!matriculas.Any()) return NotFound();

            return Ok(new { matriculas = matriculas });
        }

        [HttpPost]
        [Route("{turmaId}/{alunoId}")]
        public async Task<IActionResult> Matricular(Guid turmaId, Guid alunoId, [FromBody] MatriculaCommand command)
        {
           
            _matriculaApplication.AddParams(turmaId, alunoId, command);
            var matriculaId = await _matriculaApplication.Matricular();            

            return Ok(new { matriculaId = matriculaId });
           
          //  return Ok();
        }

        [HttpPost]
        [Route("anotacao")]
        public async Task<IActionResult> SetAnotacao([FromBody] AnotacaoDto anotacao)
        {   
            await _matriculaApplication.SetAnotacao(anotacao);

            return Ok();

        }


    }

    
}
