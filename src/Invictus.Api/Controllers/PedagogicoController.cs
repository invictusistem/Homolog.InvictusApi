using AutoMapper;
using Invictus.Api.Model;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Pedagogico;
using Invictus.Application.Queries.Interfaces;
using Invictus.Core.Enums;
using Invictus.Core.Util;
using Invictus.Data.Context;
using Invictus.Domain;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Pedagogico;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/pedag")]
    public class PedagogicoController : ControllerBase
    {
        private readonly IPedagogicoQueries _pedagogicoQuery;
        private readonly IAgendaProvasRepository _agendaRepository;
        private readonly IFinanceiroQueries _finQueries;
        private readonly IHistoricoEscolarRepo _historicoRepo;
        private readonly IMapper _mapper;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IAlunoQueries _alunoQueries;
        private readonly InvictusDbContext _context;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly string unidade;

        public PedagogicoController(IPedagogicoQueries pedagogicoQuery, IMapper mapper,
            IAgendaProvasRepository agendaRepository, IHistoricoEscolarRepo historicoRepo,
            InvictusDbContext context, IHttpContextAccessor userHttpContext,
            ITurmaQueries turmaQueries, IFinanceiroQueries finQueries, IAlunoQueries alunoQueries)
        {
            _pedagogicoQuery = pedagogicoQuery;
            _mapper = mapper;
            _agendaRepository = agendaRepository;
            _historicoRepo = historicoRepo;
            _context = context;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _turmaQueries = turmaQueries;
            _finQueries = finQueries;
            _alunoQueries = alunoQueries;
        }

        #region GET


        [HttpGet]
        [Route("pendencias-doc")]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> GetDocPendencias()
        {
            //Thread.Sleep(3000); docEnviado analisado
            var docs = await _context.DocumentosAlunos.Where(d => d.Analisado == false).ToListAsync();

            return Ok(new { qntDocs = docs.Count() });
        }

        [HttpGet]
        [Route("pendencias-lista")]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> GetDocPendenciasLista()
        {
            //Thread.Sleep(3000); docEnviado analisado
            var alunosIdsDocs = _context.DocumentosAlunos.Where(d => d.Analisado == false & d.DocEnviado == true).DistinctBy(d => d.AlunoId).Select(d => d.AlunoId).ToList();

            var alunos = new List<AlunoDto>();

            foreach (var item in alunosIdsDocs)
            {
                alunos.Add(await _alunoQueries.GetAluno(item));
            }

            foreach (var item in alunos)
            {
                item.unidadeCadastrada = _context.DocumentosAlunos.Where(d => d.Analisado == false & d.DocEnviado == true & d.AlunoId == item.id).Count();
            }



            return Ok(new { alunos = alunos });
        }

        [HttpGet]
        [Route("docs-relatorio/{alunoId}")]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> GetDocPendenciasView(int alunoId)
        {
            var docsEnviados = await _context.DocumentosAlunos.Where(d => d.DocEnviado == true & d.AlunoId == alunoId).ToListAsync();
            var docsPendentes = await _context.DocumentosAlunos.Where(d => d.DocEnviado == false & d.AlunoId == alunoId).ToListAsync();

            return Ok(new { docsEnviados = docsEnviados, docsPendentes = docsPendentes });
        }

        [HttpGet]
        //[Route("{unidade}")]
        public async Task<ActionResult<IEnumerable<TurmaViewModel>>> GetTurmas()
        {
            //Thread.Sleep(3000);
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var turmas = await _pedagogicoQuery.GetTurmas(unidadeId);

            var data = DateTime.Now;
            foreach (var item in turmas)
            {
                var temAula = await _context.Calendarios.Where(c => c.DiaAula.Year == data.Year & c.DiaAula.Month == data.Month & c.DiaAula.Day == data.Day & c.TurmaId == item.id).ToListAsync();

                if (temAula.Count() > 0)
                {
                    item.podeIniciar = true;
                    item.calendarioId = temAula[0].Id;
                }
                else
                {
                    item.podeIniciar = false;
                    item.calendarioId = 0;
                }
            }

            return Ok(new { turmas = turmas });
        }

        [HttpGet]
        [Route("materias")]
        public async Task<IEnumerable<MateriaDto>> GetMaterias()
        {
            var materias = await _pedagogicoQuery.GetMaterias();

            return materias;
        }

        [HttpGet]
        [Route("agendas/{turmaId}")]
        public async Task<IEnumerable<AgendasProvasDto>> GetMaterias(int turmaId)
        {
            var materias = await _pedagogicoQuery.GetAgendasProvas(turmaId);

            return materias;
        }

        [HttpGet]
        [Route("materias-professor/{turmaId}")]
        public async Task<ActionResult> GetMateriasDoProfessor(int turmaId)
        {
            var profId = 1;
            var materias = await _pedagogicoQuery.GetMateriasDoProfessor(turmaId, profId);

            var turma = await _context.Turmas.FindAsync(turmaId);
            var materiasModulo = await _context.Materias.Where(m => m.ModuloId == turma.ModuloId).ToListAsync();

            //return materias;
            return Ok(materiasModulo);
        }

        [HttpGet]
        [Route("materias-agenda/{turmaId}/{materiaId}")]
        public ActionResult<IEnumerable<ProvasAgenda>> GetAgendaMateria(int turmaId, int materiaId)
        {
            //var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var agenda = _context.ProvasAgenda.Where(c => c.TurmaId == turmaId & c.MateriaId == materiaId).FirstOrDefault();

            //today.AddDays(1);
            //var materias = await _pedagogicoQuery //.GetMateriasDoProfessor(turmaId, profId);

            return Ok(agenda);
        }

        [HttpGet]
        [Route("materiasturma/{turmaId}")]
        public async Task<List<NotasViewModel>> GetMateriasTurma(int turmaId)
        {
            var materias = await _pedagogicoQuery.GetNotaAlunos(turmaId);

            return materias;
        }

        [HttpGet]
        [Route("notasboletim/{turmaId}/{alunoId}")]
        public async Task<ActionResult<IEnumerable<NotasDisciplinas>>> GetNotasBoletim(int turmaId, int alunoId)
        {
            var boletim = await _context.NotasDisciplinas.Where(n => n.AlunoId == alunoId).ToListAsync();

            return Ok(new { boletim = boletim.OrderBy(b => b.MateriaId) });
        }

        [HttpGet]
        [Route("notaalunos/{turmaId}/{materiaId}")]
        public async Task<ActionResult<IEnumerable<NotasDisciplinas>>> GetNotas(int turmaId, int materiaId)
        {
            var notas = await _pedagogicoQuery.GetNotaByMateriaAndTurmaId(materiaId, turmaId);

            return Ok(notas);
        }

        [HttpGet]
        [Route("transfinterna")]
        public async Task<ActionResult> SearchAluno([FromQuery] string CPF)
        {
            //CPF = CPF.Replace(".", "").Replace("-", "");

            // TODO
            // string unidade = "CGI";
            var unidadeAtual = await _context.Unidades.Where(u => u.Sigla == unidade).FirstOrDefaultAsync();
            //var materias = await _pedagogicoQuery.GetNotaAlunos(turmaId);
            var aluno = await _context.Alunos.Where(aluno => aluno.CPF == CPF).SingleOrDefaultAsync();

            if (aluno == null) { return NotFound(new { message = "Nenhum Aluno foi localizado com este CPF." }); }

            if (aluno != null)
            {
                // bool daMesmaUnidade = aluno.UnidadeCadastrada == unidade;
                bool daMesmaUnidade = aluno.UnidadeCadastrada == unidadeAtual.Id;
                if (daMesmaUnidade)
                {
                    return Conflict(new { message = "O Aluno já está matriculado/cadastrado nesta unidade." });

                }

            }

            // VERIFICAR PENDENCIA ALUNO

            //Unidades uni = Unidades.CGI;

            // var turmas = _context.Turmas.Where(t => t.Unidade == uni & t.TotalAlunos < t.Vagas).ToList();
            var turmas = await _turmaQueries.GetTurmasComVagas(unidadeAtual.Id);

            if (turmas.Count() > 0)
            {
                var turmaOld = await _turmaQueries.GetTurmasMatriculadosOutraUnidade(aluno.Id, aluno.UnidadeCadastrada);

                if (turmaOld.Count() == 0) return Conflict(new { message = "O Aluno está cadastrado em outra unidade, porém não matriculado." });

                var debitos = await _finQueries.GetDebitoAlunos(aluno.Id, turmaOld.ToArray()[0].id);
                // Em aberto
                var hoje = DateTime.Now;
                var temdebitoVencido = debitos.Where(d => d.status == "Vencido" & d.dataVencimento < hoje);

                if (temdebitoVencido.Count() > 0)
                {
                    return Ok(new { aluno = aluno, turmas = turmas, debitos = true });
                }
                //else
                //{

                //}
            }
            //var capacidade = _context.Salas.Where(s => s.Id == turmas)


            return Ok(new { aluno = aluno, turmas = turmas, debitos = false });
        }


        [HttpGet]
        [Route("transf-turma")]
        public async Task<ActionResult> SearchAlunoTurma([FromQuery] string CPF)
        {
            //CPF = CPF.Replace(".", "").Replace("-", "");

            // TODO
            // string unidade = "CGI";
            var unidadeAtual = await _context.Unidades.Where(u => u.Sigla == unidade).FirstOrDefaultAsync();
            //var materias = await _pedagogicoQuery.GetNotaAlunos(turmaId);
            var aluno = await _context.Alunos.Where(aluno => aluno.CPF == CPF).SingleOrDefaultAsync();

            var alunoNaUnidade = aluno.UnidadeCadastrada == unidadeAtual.Id;

            if (!alunoNaUnidade) { return Ok(new { message = "Aluno de outra unidade." }); }

            var matriculadoEmAlguma = await _context.Matriculados.Where(m => m.AlunoId == aluno.Id).SingleOrDefaultAsync();

            if (matriculadoEmAlguma == null) { return Ok(new { message = "Aluno matriculado em nenhuma turma." }); }

            var turmaDaunidade = await _context.Turmas.Where(t => t.Id == matriculadoEmAlguma.TurmaId & t.UnidadeId == unidadeAtual.Id).SingleOrDefaultAsync();

            if (turmaDaunidade == null) { return Ok(new { message = "Turma de outra unidade." }); }

            var turmaComVagasParaTRansferencia = await _context.Turmas.Where(t => t.Id != matriculadoEmAlguma.TurmaId).ToListAsync();

            if (turmaComVagasParaTRansferencia.Count == 0) { return Ok(new { message = "Sem turmas para transferência" }); }

            return Ok(new { aluno = aluno, turmas = turmaComVagasParaTRansferencia, debitos = false, turmaAtualId = matriculadoEmAlguma.TurmaId });
        }

        [HttpGet]
        [Route("presenca-lista/{turmaId}")]
        public async Task<ActionResult> GetPresencaLista(int turmaId)
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 3, 0, 0);
            var calendar = await _context.Calendarios.Where(c => c.DiaAula == hoje).SingleOrDefaultAsync();
            if (calendar == null) return Ok();
            var notas = await _pedagogicoQuery.GetInfoDiaPresencaLista(calendar.MateriaId, turmaId, calendar.Id);
            // calendar.SetDataInicioAula();
            //calendar.IniciarAula();
            // _context.Calendarios.Update(calendar);
            //_context.SaveChanges();
            return Ok(new { infos = notas.infos, lista = notas.listaPresencas });
        }

        [HttpPost]
        [Route("presenca-lista")]
        public IActionResult SavePresencaLista([FromBody] SavePresencaCommand savePresencaCommand)
        {

            foreach (var item in savePresencaCommand.listaPresencaDto)
            {
                if (item.isPresentToString.ToLower() == "f")
                {
                    item.isPresent = true;
                }
                else
                {
                    item.isPresent = false;
                }
            }
            foreach (var item in savePresencaCommand.listaPresencaDto)
            {
                var presencas = new LivroPresenca(item.id, item.calendarioId, item.isPresent, item.alunoId, item.isPresentToString);

                _context.LivroPresencas.Update(presencas);
                _context.SaveChanges();
            }

            var calendario = _context.Calendarios.Find(savePresencaCommand.calendarId);
            calendario.SetObservacoes(savePresencaCommand.observacoes);
            calendario.SetDataConclusaoAula();
            calendario.ConcluirAula();

            _context.Calendarios.Update(calendario);
            _context.SaveChanges();

            return Ok();
        }

        #endregion


        [HttpPost]
        [Route("transfinterna")]
        public IActionResult TransInterna([FromQuery] int alunoId, [FromQuery] int turmaId, [FromQuery] int turmaIdAntiga)
        {

            // change aluno.UnidadeCadastrada
            // Debito.IdUnidadeResponsavel nos debitos em aberto (nos vencidos tem q pagar antes ou parcelar)
            // LivroMatriculas.TurmaId
            // HistoricoEscola.TurmaId
            // Matriculados.TurmaId
            // NotasDisciplinas.TurmaId onde já tem notas

            var turmaNova = _context.Turmas.Find(turmaId);
            turmaNova.AddAlunoNaTurma();
            _context.Turmas.Update(turmaNova);
            // TODO: retirar da turma antiga
            //var turmaAntiga = _context.Turmas.Find(turmaId);

            // TODO: REFACT para EDITAR MATRICULADOS
            var aluno = _context.Alunos.Find(alunoId);
            var mat = new Matriculados(0, aluno.Id, aluno.Nome, aluno.CPF, "Matriculado", turmaNova.Id);

            var TotalAlunos = _context.Alunos.Count();
            mat.SetNumeroMatricula(TotalAlunos);

            mat.SetDiaMatricula();
            _context.Matriculados.Add(mat);

            _context.SaveChanges();



            return Ok();
        }

        [HttpPost]
        [Route("trans-turma")]
        public IActionResult TransTurma([FromQuery] int alunoId, [FromQuery] int turmaId, [FromQuery] int turmaIdAntiga)
        {

            var matriculado = _context.Matriculados.Where(m => m.AlunoId == alunoId & m.TurmaId == turmaIdAntiga).SingleOrDefault();
            matriculado.SetTurmaId(turmaId);
            _context.Matriculados.Update(matriculado);

            var historicoEscolar = _context.HistoricosEscolares.Where(h => h.AlunoId == alunoId & h.TurmaId == turmaIdAntiga).SingleOrDefault();
            historicoEscolar.SetTurmaId(turmaId);
            _context.HistoricosEscolares.Update(historicoEscolar);

            var notasDisciplinas = _context.NotasDisciplinas.Where(m => m.AlunoId == alunoId & m.TurmaId == turmaIdAntiga).ToList();
            foreach (var notas in notasDisciplinas)
            {
                notas.SetTurmaId(turmaId);
            }
            _context.NotasDisciplinas.UpdateRange(notasDisciplinas);

            var documentosAluno = _context.DocumentosAlunos.Where(m => m.AlunoId == alunoId & m.TurmaId == turmaIdAntiga).ToList();
            foreach (var doc in documentosAluno)
            {
                doc.SetTurmaId(turmaId);
            }
            _context.DocumentosAlunos.UpdateRange(documentosAluno);

            var infoFinanc = _context.InfoFinanceiras.Where(m => m.AlunoId == alunoId & m.TurmaId == turmaIdAntiga).SingleOrDefault();
            infoFinanc.SetTurmaId(turmaId);
            _context.InfoFinanceiras.Update(infoFinanc);

            var turmaAntiga = _context.Turmas.Find(turmaIdAntiga);
            turmaAntiga.RemoveAlunoFromTurma();
            _context.Turmas.Update(turmaAntiga);

            var turmaNova = _context.Turmas.Find(turmaId);
            turmaNova.AddAlunoNaTurma();
            _context.Turmas.Update(turmaNova);

            _context.SaveChanges();


            return Ok();
        }

        [HttpGet]
        [Route("transfinterna/verificar")]
        public IActionResult VerificarTransferencia([FromQuery] string CPF)
        {
            //var materias = await _pedagogicoQuery.GetNotaAlunos(turmaId);
            var aluno = _context.Alunos.Where(aluno => aluno.CPF == CPF).SingleOrDefault();

            if (aluno == null) { return Ok(new { message = "Nenhum Aluno foi localizado com este CPF." }); }

            if (aluno != null)
            {
                bool daMesmaUnidade = aluno.UnidadeCadastrada == 1;//.UnidadeCadastrada == "CGI";
                if (daMesmaUnidade)
                {
                    //var curso = _context.AlunosTurma.Where(aluno => aluno.AlunoId == aluno.Id).SingleOrDefault();

                    //if (curso != null)
                    //{
                    //    return Ok(new { message = "O Aluno já está matriculado em um curso nesta unidade." });
                    //}
                }

            }

            return Ok(aluno);
        }


        #region PUT

        [HttpPut]
        [Route("notaalunos")]
        public IActionResult PutNotaAlunos([FromBody] List<NotasDisciplinasDtoV2> notas)
        {
            var notasDisciplinas = Convert(notas);

            foreach (var nota in notasDisciplinas)
            {
                var notasDisc = _mapper.Map<NotasDisciplinas>(nota);

                notasDisc.VerificarStatusResultado();
                _context.NotasDisciplinas.Update(notasDisc);
            }



            _context.SaveChanges();


            //var listNotas = new List<BoletimEscolar>();

            //foreach (var item in notas)
            //{
            //    foreach (var nota in item.alunos)
            //    {
            //        if (String.IsNullOrWhiteSpace(nota.av1)) { nota.av1 = null; }
            //        if (String.IsNullOrWhiteSpace(nota.av2)) { nota.av2 = null; }
            //        if (String.IsNullOrWhiteSpace(nota.av3)) { nota.av3 = null; }

            //    }
            //}



            return Ok();
        }

        public List<NotasDisciplinas> Convert(List<NotasDisciplinasDtoV2> notas)
        {
            var notasDisc = new List<NotasDisciplinas>();

            foreach (var nota in notas)
            {
                var resultado = ResultadoNotas.TryParse(nota.resultado);
                notasDisc.Add(new NotasDisciplinas(nota.id, nota.trimestre, nota.avaliacaoUm, nota.segundaChamadaAvaliacaoUm, nota.avaliacaoDois,
                    nota.segundaChamadaAvaliacaoDois, nota.avaliacaoTres, nota.segundaChamadaAvaliacaoTres, nota.materiaId, nota.materiaDescricao,
                    nota.alunoId, nota.turmaId, resultado));
            }

            return notasDisc;
        }

        [HttpPut]
        [Route("agendas")]
        public IActionResult EditAgendaProof([FromBody] AgendasProvasDto agenda)
        {
            var agendaMod = _mapper.Map<AgendasProvasDto, ProvasAgenda>(agenda);

            _agendaRepository.EditAgenda(agendaMod);

            return Ok();
        }

        [HttpPut]
        [Route("doc-analisar/{docId}/{analise}")]
        public async Task<IActionResult> DocAnalisar(int docId, bool analise)
        {

            var doc = await _context.DocumentosAlunos.FindAsync(docId);
            doc.ValidarDoc(analise);

            await _context.DocumentosAlunos.SingleUpdateAsync(doc);
            return Ok();
        }

        #endregion


    }
}