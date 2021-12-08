using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/turma")]
    [Authorize]
    [ApiController]
    public class PedagTurmaController : ControllerBase
    {
        private readonly IProfessorQueries _profQueries;
        private readonly ITurmaApplication _turmaApp;
        private readonly ITurmaQueries _turmaQueries;
        public PedagTurmaController(IProfessorQueries profQueries, ITurmaApplication turmaApp, ITurmaQueries turmaQueries)
        {
            _profQueries = profQueries;
            _turmaApp = turmaApp;
            _turmaQueries = turmaQueries;
        }

        [HttpGet]
        [Route("professores/{turmaId}")]
        public async Task<IActionResult> GetFile(Guid turmaId)
        {
            var profs = await _profQueries.GetProfessoresDisponiveis(turmaId);
            if (profs.Count() == 0) return NotFound();

            return Ok(new { profs = profs });
        }

        [HttpPost]
        [Route("Professores")]
        public async Task<IActionResult> SaveProfsInTurma([FromBody] SaveProfsCommand command)
        {

            await _turmaApp.AddProfessoresNaTurma(command);

            return Ok();
        }

        [HttpGet]
        [Route("materias/{turmaId}/{professorId}")]
        public async Task<IActionResult> GetMateriasLiberadas(Guid turmaId, Guid professorId)
        {

            var matsView = await _turmaQueries.GetMateriasLiberadas(turmaId, professorId);//.AddProfesso resNaTurma(command);

            return Ok(new { matsView = matsView });
        }

        [HttpPut]
        [Route("materias/{turmaId}/{professorId}")]
        public async Task<IActionResult> SetProfessorNaTurma(Guid turmaId, Guid professorId, [FromBody] IEnumerable<MateriaView> profsMatCommand)
        {
            await _turmaApp.SetMateriaProfessor(turmaId, professorId, profsMatCommand);

            return Ok();
        }

        [HttpDelete]
        [Route("professor/{professorId}/{turmaId}")]
        public async Task<IActionResult> ExcluirProfessorDaTurma(Guid professorId, Guid turmaId)
        {
            await _turmaApp.RemoverProfessorDaTurma(professorId, turmaId);

            return Ok();
        }

    }
}
