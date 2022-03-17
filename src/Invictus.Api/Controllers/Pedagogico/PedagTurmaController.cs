using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
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
        private readonly IPedagogicoApplication _pedagApp;
        private readonly ITurmaQueries _turmaQueries;
        private readonly ICalendarioQueries _calendarioQueries; 
        private readonly ITurmaPedagQueries _turmaPedagQueries;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly ICalendarioApp _calendarioApp;
        public PedagTurmaController(IProfessorQueries profQueries, ITurmaApplication turmaApp, ITurmaQueries turmaQueries,
            ICalendarioQueries calendarioQueries, ITurmaPedagQueries turmaPedagQueries, IPedagogicoApplication pedagApp,
            IUnidadeQueries unidadeQueries, ICalendarioApp calendarioApp)
        {
            _profQueries = profQueries;
            _turmaApp = turmaApp;
            _turmaQueries = turmaQueries;
            _calendarioQueries = calendarioQueries;
            _turmaPedagQueries = turmaPedagQueries;
            _pedagApp = pedagApp;
            _unidadeQueries = unidadeQueries;
            _calendarioApp = calendarioApp;
        }

        [HttpGet]
        public async Task<IActionResult> GetTurmas()
        {
            var turmas = await _turmaQueries.GetTurmasPedagViewModel();

            if (turmas.Count() == 0) return NotFound();

            return Ok(new { turmas = turmas });
        }

        [HttpGet]
        [Route("calendario/{turmaId}")]
        public async Task<ActionResult> GetCalendarios(Guid turmaId)
        {
            var calends = await _calendarioQueries.GetCalendarioByTurmaId(turmaId);

            return Ok(new { calends = calends });
        }

        //[HttpGet]
        //[Route("calendario/{turmaId}")]
        //public async Task<ActionResult> GetCalendariosPAGINATORTESTE(Guid turmaId)
        //{
        //    var calends = await _calendarioQueries.GetCalendarioByTurmaId(turmaId);

        //    return Ok(new { calends = calends });
        //}

        [HttpGet]
        [Route("aula/{calendarioId}")]
        public async Task<ActionResult> GetCalendario(Guid calendarioId)
        {
            var aula = await _calendarioQueries.GetAulaViewModel(calendarioId);

            return Ok(new { aula = aula });
        }

        [HttpGet]
        [Route("aula-edit/{calendarioId}")]
        public async Task<ActionResult> GetAulaEditViewModel(Guid calendarioId)
        {
            var aula = await _calendarioQueries.GetAulaViewModel(calendarioId);

            var profsDisponiveis = await _profQueries.GetProfessoresDisponiveisByFilter(aula.diaDaSemana, aula.unidadeId, aula.materiaId);
            
            var materias = await _turmaQueries.GetMateriasDaTurma(aula.turmaId);

            var salas = await _unidadeQueries.GetSalas(aula.unidadeId);

            return Ok(new { aula = aula, profsDisponiveis = profsDisponiveis, materias = materias, salas = salas });
        }

        [HttpGet]
        [Route("aula-edit/profs/{calendarioId}/{materiaId}")]
        public async Task<ActionResult> GetProfsDisponiveis(Guid calendarioId, Guid materiaId)
        {
            var aula = await _calendarioQueries.GetAulaViewModel(calendarioId);

            var profsDisponiveis = await _profQueries.GetProfessoresDisponiveisByFilter(aula.diaDaSemana, aula.unidadeId, materiaId);

            if (profsDisponiveis.Count() == 0) return NotFound();

            return Ok(new { profsDisponiveis = profsDisponiveis });
        }


        [HttpGet]
        [Route("professores/{turmaId}")]
        public async Task<IActionResult> GetFile(Guid turmaId)
        {
            var profs = await _profQueries.GetProfessoresDisponiveis(turmaId);
            if (profs.Count() == 0) return NotFound();

            return Ok(new { profs = profs });
        }

        [HttpGet]
        [Route("notas/{turmaId}/{materiaId}")]
        public async Task<IActionResult> GetNotasDasTurmasPorMateria(Guid turmaId, Guid materiaId)
        {
            var notas = await _turmaPedagQueries.GetNotasFromTurma(turmaId, materiaId);

            return Ok(new { notas = notas });
        }

        [HttpGet]
        [Route("materias/{turmaId}/{professorId}")]
        public async Task<IActionResult> GetMateriasLiberadas(Guid turmaId, Guid professorId)
        {

            var matsView = await _turmaQueries.GetMateriasLiberadas(turmaId, professorId);//.AddProfesso resNaTurma(command);

            return Ok(new { matsView = matsView });
        }

        [HttpGet]
        [Route("presenca-lista/{calendarioId}")]
        public async Task<ActionResult> GetPresencaLista(Guid calendarioId)
        {

            var presencas = await _turmaQueries.GetInfoDiaPresencaLista(calendarioId);//.GetInfoDiaPresencaLista
            //var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 3, 0, 0);
            ////var calendar = await _context.Calendarios.Where(c => c.DiaAula == hoje).SingleOrDefaultAsync();
            //var calendar = await _context.Calendarios.Where(c => c.DiaAula == hoje).FirstOrDefaultAsync();
            //if (calendar == null) return Ok();
            //var notas = await _pedagogicoQuery.GetInfoDiaPresencaLista(calendar.MateriaId, turmaId, calendar.Id);

            //return Ok(new { infos = notas.infos, lista = notas.listaPresencas });

            return Ok(new { presencas = presencas });
        }

        [HttpGet]
        [Route("presenca-diario/{calendarioId}")]
        public async Task<ActionResult> GetAulaDiarioClasse(Guid calendarioId)
        {
            // verificar se pode iniciar aula = pelo turma, dia, horario e quem está iniciando

            var presencas = await _turmaQueries.GetPresencaAulaViewModel(calendarioId);

            return Ok(new { presencas = presencas });
        }

        //[HttpPost]
        //[Route("presenca-diario/{calendarioId}")]
        //public async Task<ActionResult> SaveDiarioDeClasse([FromBody] AulaDiarioClasseViewModel aulaView, Guid calendarioId)
        //{
        //    // verificar se pode iniciar aula = pelo turma, dia, horario e quem está iniciando

        //    //var presencas = await _turmaQueries.GetPresencaAulaViewModel(calendarioId);
        //    retu rn  Ok();
        //    //return Ok(new { presencas = presencas });
        //}

        [HttpPost]
        [Route("presenca-diario/{calendarioId}")]
        public async Task<ActionResult> SaveDiarioDeClasse([FromBody] AulaDiarioClasseViewModel saveCommand, Guid calendarioId)
        {
            // verificar se pode iniciar aula = pelo turma, dia, horario e quem está iniciando
            await _turmaApp.SavePresenca(saveCommand);
            //var presencas = await _turmaQueries.GetPresencaAulaViewModel(calendarioId);
            return Ok();
            //return Ok(new { presencas = presencas });
        }

        [HttpPut]
        [Route("presenca-diario/{calendarioId}")]
        public async Task<ActionResult> UpdateDiarioDeClasse([FromBody] AulaDiarioClasseViewModel saveCommand, Guid calendarioId)
        {
            // verificar se pode iniciar aula = pelo turma, dia, horario e quem está iniciando
            await _turmaApp.SavePresenca(saveCommand);
            //var presencas = await _turmaQueries.GetPresencaAulaViewModel(calendarioId);
            return Ok();
            //return Ok(new { presencas = presencas });
        }

        [HttpPost]
        [Route("Professores")]
        public async Task<IActionResult> SaveProfsInTurma([FromBody] SaveProfsCommand command)
        {
            await _turmaApp.AddProfessoresNaTurma(command);

            return Ok();
        }

        [HttpPut]
        [Route("calendario/editar/{calendarioId}")]
        public async Task<IActionResult> EditCalendario([FromBody] AulaViewModel aula, Guid calendarioId)
        {
            // await _turmaApp.SetMa teriaProfessor(turmaId, professorId, profsMatCommand);

            var aulaSaved = await _calendarioApp.EditCalendario(aula, calendarioId);

            return Ok(new { aula = aulaSaved });
            //return Ok();
        }

        [HttpPut]
        [Route("materias/{turmaId}/{professorId}")]
        public async Task<IActionResult> SetProfessorNaTurma(Guid turmaId, Guid professorId, [FromBody] IEnumerable<MateriaView> profsMatCommand)
        {
            await _turmaApp.SetMateriaProfessor(turmaId, professorId, profsMatCommand);

            return Ok();
        }

        [HttpPut]
        [Route("notas")]
        public async Task<IActionResult> PutNotaAlunos([FromBody] List<TurmaNotasDto> notas)
        {
            await _turmaApp.UpdateNotas(notas);

            return Ok();
        }

        [HttpPut]
        [Route("calendario/{calendarioId}")]
        public async Task<IActionResult> IniciarAula(Guid calendarioId)
        {
            // TODO REVERIFICAR SE PODE INICIAR! SE TA NOA HORA CERTA E SE É AUTORIZADO
            await _pedagApp.IniciarAula(calendarioId);

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
