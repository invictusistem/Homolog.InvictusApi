using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/turma")]
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
        public TurmaController(IUnidadeQueries unidadeQueries, ITypePacoteQueries typeQueries, ITurmaApplication turmaApplication,
            ITurmaQueries turmaQueries, IPlanoPagamentoQueries planosQueries, IAlunoQueries alunoQueries, ITurmaPedagQueries turmaPedagQueries)
        {
            _unidadeQueries = unidadeQueries;
            _typeQueries = typeQueries;
            _turmaApplication = turmaApplication;
            _turmaQueries = turmaQueries;
            _planosQueries = planosQueries;
            _alunoQueries = alunoQueries;
            _turmaPedagQueries = turmaPedagQueries;
        }


        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> GetCreateTurmaViewModel()
        {
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

            return Ok(new { alunos = alunos, professores = professores });//Ok(new { salas = salas, typePacotes = typePacotes });

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

        /*
          var turma = _db.Turmas.Find(turmaId);
            turma.IniciarTurma();
            _db.Turmas.Update(turma);
            _db.SaveChanges();
         */
    }
}
