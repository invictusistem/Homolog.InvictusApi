using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Administrativo
{
    [Route("api/alunos")]
    [Authorize]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoQueries _alunoQueries;
        private readonly IAlunoApplication _alunoApplication;
        private readonly IUtils _utils;
        public AlunoController(IAlunoQueries alunoQueries, IAlunoApplication alunoApplication, IUtils utils)
        {
            _alunoQueries = alunoQueries;
            _alunoApplication = alunoApplication;
            _utils = utils;
        }       

        [HttpGet]
        [Route("pesquisar")]
        public async Task<IActionResult> GetAlunoByFilter([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            // TODO? trazer unidade tb
            var results = await _alunoQueries.GetMatriculadosView(itemsPerPage, currentPage, paramsJson);

            if (results.Data.Count() == 0) return NotFound();

            return Ok(results);
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<IActionResult> PesquisarAluno(string cpf)
        {
            var aluno = await _alunoQueries.SearchPerCPF(cpf);

            if (aluno.Count() > 0) return Conflict();

            return Ok();
        }

        [HttpGet]
        [Route("cadastro/{alunoId}")]
        public async Task<IActionResult> GetAlunoById(Guid alunoId)
        {
            var aluno = await _alunoQueries.GetAlunoById(alunoId);

           // if (aluno.Count() > 0) return Conflict();

            return Ok(new { aluno = aluno });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAluno([FromBody] AlunoDto newAluno)
        {
            //var aluno = await _alunoQueries.FindAlunoByCPForEmailorRG(newAluno.cpf, newAluno.rg, newAluno.email);
            var msg = await _utils.ValidaDocumentosAluno(newAluno.cpf, newAluno.rg, newAluno.email);

            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _alunoApplication.saveAlunos(newAluno);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditAluno([FromBody] AlunoDto aluno)
        {
            await _alunoApplication.EditAluno(aluno);

            return Ok();
        }
    }
}
