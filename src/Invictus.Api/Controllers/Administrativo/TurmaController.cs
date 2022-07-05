using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Enumerations.Logs;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Logs;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/turma")]
    [Authorize]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly ITypePacoteQueries _typeQueries;
        private readonly ITurmaApplication _turmaApplication;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IPlanoPagamentoQueries _planosQueries;
        private readonly ITurmaPedagQueries _turmaPedagQueries;
        private readonly IAspNetUser _netUser;
        private readonly InvictusDbContext _db;
        public TurmaController(IUnidadeQueries unidadeQueries, ITypePacoteQueries typeQueries, ITurmaApplication turmaApplication,
            ITurmaQueries turmaQueries, IPlanoPagamentoQueries planosQueries, IAlunoQueries alunoQueries, ITurmaPedagQueries turmaPedagQueries,
            InvictusDbContext db, IAspNetUser netUser)
        {
            _unidadeQueries = unidadeQueries;
            _typeQueries = typeQueries;
            _turmaApplication = turmaApplication;
            _turmaQueries = turmaQueries;
            _planosQueries = planosQueries;
            _alunoQueries = alunoQueries;
            _turmaPedagQueries = turmaPedagQueries;
            _db = db;
            _netUser = netUser;
        }


        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> GetCreateTurmaViewModel()
        {
            // return BadRequest();
            var salas = await _unidadeQueries.GetSalasByUserUnidade();

            var typePacotes = await _typeQueries.GetTypePacotes();

            if (salas.Count() == 0 || typePacotes.Count() == 0) return NotFound();

            return Ok(new { salas = salas, typePacotes = typePacotes });

        }

        [HttpGet]
        [Route("info/{turmaId}")]
        public async Task<IActionResult> GetTurmaEditViewModel(Guid turmaId)
        {
            var alunos = await _turmaPedagQueries.GetAlunosDaTurma(turmaId);

            var turma = await _turmaQueries.GetTurmaInfo(turmaId);

            var professores = await _turmaQueries.GetProfessoresDaTurma(turmaId);

            // if (salas.Count() == 0 || typePacotes.Count() == 0) return NotFound();

            return Ok(new { alunos = alunos.OrderBy(a => a.nome), professores = professores, turma = turma.FirstOrDefault() });//Ok(new { salas = salas, typePacotes = typePacotes });

        }

        [HttpGet]
        public async Task<IActionResult> GetTurmas()
        {
            var turmas = await _turmaQueries.GetTurmas();

            if (turmas.Count() == 0) return NotFound();

            return Ok(new { turmas = turmas });

        }

        [HttpGet]
        [Route("get/{turmaId}/{alunoId}")]
        public async Task<IActionResult> GetTurma(Guid turmaId, Guid alunoId)
        {
            // get Turma
            var turma = await _turmaQueries.GetTurmasById(turmaId);

            // get PlanOS de pag baseado no typePacoteId pego nda turma
            var planos = await _planosQueries.GetPlanosByTypePacote(turma.typePacoteId);

            var idade = await _alunoQueries.GetIdadeAluno(alunoId);

            int age = 0;
            age = DateTime.Now.Subtract(idade).Days;
            age = age / 365;
            var menor = true;
            if (age >= 18) menor = false;

            return Ok(new { turma = turma, planos = planos, menor = menor });

        }

        [HttpGet]
        [Route("{typePacoteId}")]
        public async Task<IActionResult> GetTurmasByTypePacoteId(Guid typePacoteId)
        {
            var turmas = await _turmaQueries.GetTurmasByType(typePacoteId);

            if (turmas.Count() == 0) return NotFound();

            return Ok(new { turmas = turmas });

        }

        [HttpGet]
        [Route("materias/{turmaId}")]
        public async Task<IActionResult> GetMateriasDaTurma(Guid turmaId)
        {
            var materias = await _turmaQueries.GetMateriasDaTurma(turmaId);

            if (materias.Count() == 0) return NotFound();

            return Ok(new { materias = materias });
        }

        [HttpGet]
        [Route("materias-notas/{turmaId}")]
        public async Task<IActionResult> GetNotas(Guid turmaId)
        {
            var materias = await _turmaQueries.GetMateriasDoProfessorLiberadasParaNotas(turmaId);

            if (!materias.Any()) return NotFound();

            return Ok(new { materias = materias });

        }

        [HttpGet]
        [Route("materias-notas-v2/{turmaId}")]
        public async Task<IActionResult> GetNotasV2(Guid turmaId)
        {
            var materias = await _turmaQueries.GetMateriasDoProfessorLiberadasParaNotasV2(turmaId);
            
            if (!materias.Any()) return NotFound();

            return Ok(new { materias = materias });

        }

        [HttpPost]
        public async Task<IActionResult> CreateTurma([FromBody] CreateTurmaCommand command)
        {
            await _turmaApplication.CreateTurma(command);

            return Ok();

        }

        [HttpPut]
        [Route("iniciar/{turmaId}")]
        public async Task<IActionResult> IniciarTurma(Guid turmaId)
        {
            await _turmaApplication.IniciarTurma(turmaId);

            return Ok();

        }

        [HttpPut]
        [Route("adiar/{turmaId}")]
        public async Task<IActionResult> AdiarTurma(Guid turmaId)
        {
            await _turmaApplication.AdiarInicio(turmaId);

            return Ok();

        }

        [HttpPut]
        [Route("cancelar/{turmaId}")]
        public async Task<IActionResult> Cancelar(Guid turmaId)
        {
            var turma = await _db.Matriculas.Where(t => t.TurmaId == turmaId).ToListAsync();

            if (turma.Any()) return Conflict();

            var userId = _netUser.ObterUsuarioId();
            var unidadeId = _netUser.GetUnidadeIdDoUsuario();
            var logTurma = new LogTurmas(turmaId, LogTurmaAcao.Cancelamento, userId, DateTime.Now, unidadeId, "");

            await _db.LogTurmas.AddAsync(logTurma);

            var calendariosDaTurma = await _db.Calendarios.Where(c => c.TurmaId == turmaId).ToListAsync();

            var turmaHorarios = await _db.Horarios.Where(c => c.TurmaId == turmaId).ToListAsync();
            _db.Horarios.RemoveRange(turmaHorarios);
            var turmaMaterias = await _db.TurmasMaterias.Where(c => c.TurmaId == turmaId).ToListAsync();
            _db.TurmasMaterias.RemoveRange(turmaMaterias);
            var turmaNotas = await _db.TurmasNotas.Where(c => c.TurmaId == turmaId).ToListAsync();
            _db.TurmasNotas.RemoveRange(turmaNotas);
            //var turmaPresencas = await _db.Presencas.Where(c => c..TurmaId == turmaId).ToListAsync();
            var turmaPrevisoes = await _db.Previsoes.Where(c => c.TurmaId == turmaId).ToListAsync();
            _db.Previsoes.RemoveRange(turmaPrevisoes);
            var turmaProfessores = await _db.TurmasProfessores.Where(c => c.TurmaId == turmaId).ToListAsync();
            _db.TurmasProfessores.RemoveRange(turmaProfessores);
            // var turma //UPDATE

            var turmaCancelar = await _db.Turmas.FindAsync(turmaId);

            turmaCancelar.CancelarTurma();

            _db.Turmas.Update(turmaCancelar);
            try
            {
                _db.SaveChanges();
            }catch(Exception ex)
            {

            }
            return Ok();

        }


        /*
          var turma = _db.Turmas.Find(turmaId);
            turma.IniciarTurma();
            _db.Turmas.Update(turma);
            _db.SaveChanges();
         */
    }
}
