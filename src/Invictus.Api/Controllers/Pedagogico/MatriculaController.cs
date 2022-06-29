using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.RegistroMatricula;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAspNetUser _aspNetUser;
        private readonly IUtils _utils;
        private readonly InvictusDbContext _db;
        public MatriculaController(IMatriculaQueries matriculaQueries, IMatriculaApplication matriculaApplication, IPedagMatriculaQueries pedagMatriculaQueries,
            IUtils utils, InvictusDbContext db, IAspNetUser aspNetUser)
        {
            _matriculaQueries = matriculaQueries;
            _matriculaApplication = matriculaApplication;
            _pedagMatriculaQueries = pedagMatriculaQueries;
            _aspNetUser = aspNetUser;
            _utils = utils;
            _db = db;
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

            var anotacoes = await _pedagMatriculaQueries.GetAnotacoesMatricula(matriculaId);
          

            return Ok(new { anotacoes = anotacoes });
        }

        [HttpGet]
        //[Route("anotacao")]
        public async Task<IActionResult> GetMatriculadosFromUnidade()
        {
            var matriculados = await _pedagMatriculaQueries.GetMatriculadosFromUnidade();

            return Ok(new { matriculados = matriculados });
        }

        [HttpGet]
        [Route("aluno-indicacao")]
        public async Task<IActionResult> GetAlunosIndicacao()
        {
            // cadastro aluno baseado na matricula

            var alunos = await _pedagMatriculaQueries.GetAlunosIndicacao();



            return Ok(new { alunos = alunos });
        }
        #region TRANSFERENCIA
        [HttpGet]
        [Route("transf-turma/{matricula}")]
        public async Task<IActionResult> GetMatriculaTransfTurma(string matricula)
        {
            // cadastro aluno baseado na matricula

            var aluno = await _pedagMatriculaQueries.GetMatriculaByNumeroMatricula(matricula);

            if (aluno == null) return NotFound();

            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();

            if (unidadeId != aluno.unidadeId) return NotFound();

            var turmas = await _db.Turmas.Where(t => t.UnidadeId == unidadeId & t.Id != aluno.turmaId).ToListAsync();

            return Ok(new { aluno = aluno, turmas = turmas });
        }

        

        [HttpGet]
        [Route("transf-unidade/{matricula}")]
        public async Task<IActionResult> GetAlunosIndicacao(string matricula)
        {
            var matriculaAluno = await _db.Matriculas.Where(m => m.NumeroMatricula == matricula).SingleOrDefaultAsync();

            if (matriculaAluno == null) return NotFound();

            var validar = await _db.Boletos.Where(b => b.PessoaId == matriculaAluno.Id & b.StatusBoleto == StatusPagamento.Vencido.DisplayName).ToListAsync();

            var podeTransf = true;

            if (validar.Any()) podeTransf = false;

            var aluno = await _pedagMatriculaQueries.GetMatriculaByNumeroMatricula(matricula);

            var unidades = await _db.Unidades.Where(u => u.Id != aluno.unidadeId).ToListAsync();

            if (aluno == null) return NotFound();

            return Ok(new { aluno = aluno, podeTransf= podeTransf, unidades = unidades });
        }

        [HttpGet]
        [Route("transf-unidade-turmas/{matriculaId}/{unidadeId}")]
        public async Task<IActionResult> GetTurmasTransfs(Guid matriculaId, Guid unidadeId)
        {
            var matriculaAtual = await _db.Matriculas.Where(m => m.Id == matriculaId).SingleOrDefaultAsync();

            var turmas = await _db.Turmas.Where(t => t.Id != matriculaAtual.TurmaId & t.UnidadeId != unidadeId 
            & (t.StatusAndamento == StatusTurma.AguardandoInicio.DisplayName || t.StatusAndamento == StatusTurma.EmAndamento.DisplayName)).ToListAsync();

            if (!turmas.Any()) return NotFound();

            return Ok(new { turmas = turmas });
        }

        [HttpPut]
        [Route("transf-turma")]
        public async Task<IActionResult> TransfTurma([FromBody] AnotacaoDto anotacao)
        {
            //await _matriculaApplication.SetAnotacao(anotacao);

            return Ok();

        }

        [HttpPut]
        [Route("transf-unidade")]
        public async Task<IActionResult> TransfUnidade([FromBody] AnotacaoDto anotacao)
        {
            //await _matriculaApplication.SetAnotacao(anotacao);

            return Ok();

        }

        #endregion

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
