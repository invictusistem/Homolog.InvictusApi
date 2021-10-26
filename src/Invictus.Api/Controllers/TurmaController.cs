using AutoMapper;
using Invictus.Api.Model;
using Invictus.Application.Dtos;
using Invictus.Application.Dtos.Administrativo;
using Invictus.Application.Queries.Interfaces;
using Invictus.Application.Services;
using Invictus.Application.Services.Interface;
using Invictus.Core;
using Invictus.Core.Enums;
using Invictus.Core.Util;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Models;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Administrativo.UnidadeAggregate;
using Invictus.Domain.Financeiro.Aluno;
using Invictus.Domain.Financeiro.Transacoes;
using Invictus.Domain.Financeiro.VendaCurso;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate;
using Invictus.Domain.Pedagogico.HistoricoEscolarAggregate.Interfaces;
using Invictus.Domain.Pedagogico.Models;
using Invictus.Domain.Pedagogico.TurmaAggregate;
using Invictus.Domain.Pedagogico.TurmaAggregate.Interfaces;
using Invictus.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/turmas")]
    public class TurmaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICursoRepository _cursoRepository;
        private readonly IModuloQueries _moduloQueries;
        private readonly ITurmaRepository _turmaRepository;
        private readonly ICalendarioRepository _calendarioRepository;
        private readonly ICalendarioQueries _calendarioQueries;
        private readonly ICursoQueries _queries;
        private readonly IColaboradorQueries _colaboradorQueries;
        private readonly IMateriaQueries _materiaQueries;
        private readonly IAgendaProvasRepository _agendaRepository;
        private readonly ITurmaPedagRepository _turmaPedagRepository;
        private readonly IHistoricoEscolarRepo _historicoEscolarRepo;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IHttpContextAccessor _userHttpContext;
        private readonly IBoletoService _boletoService;
        private InvictusDbContext _context;
        private readonly string unidade;

        public TurmaController(IMapper mapper, ICursoRepository cursoRepository, InvictusDbContext context,
            ICursoQueries queries, ICalendarioRepository calendarioRepository, ICalendarioQueries calendarioQueries,
            IColaboradorQueries colaboradorQueries, IMateriaQueries materiaQueries, ITurmaRepository turmaRepository,
            IModuloQueries moduloQueries, IAgendaProvasRepository agendaRepository, ITurmaPedagRepository turmaPedagRepository,
            IHistoricoEscolarRepo historicoEscolarRepo, IUnidadeQueries unidadeQueries, ITurmaQueries turmaQueries,
            IHttpContextAccessor userHttpContext, IBoletoService boletoService)
        {
            _mapper = mapper;
            _cursoRepository = cursoRepository;
            _queries = queries;
            _context = context;
            _calendarioRepository = calendarioRepository;
            _calendarioQueries = calendarioQueries;
            _colaboradorQueries = colaboradorQueries;
            _materiaQueries = materiaQueries;
            _turmaRepository = turmaRepository;
            _moduloQueries = moduloQueries;
            _agendaRepository = agendaRepository;
            _turmaPedagRepository = turmaPedagRepository;
            _historicoEscolarRepo = historicoEscolarRepo;
            _unidadeQueries = unidadeQueries;
            _turmaQueries = turmaQueries;
            _userHttpContext = userHttpContext;
            unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            _boletoService = boletoService;
        }

        #region GETs

        [HttpGet]
        [Route("calendario/{turmaId}")]
        public async Task<ActionResult> GetCalendarios(int turmaId)
        {
            var calendarioTurmaView = await _calendarioQueries.GetCalendarioByTurmaId(turmaId);

            return Ok(new { calendarioTurmaView = calendarioTurmaView });
        }

        [HttpGet]
        [Route("calendario/nota-aula/{calendarioId}")]
        public async Task<ActionResult> GetNotaAula(int calendarioId)
        {
            var nota = await _context.Calendarios.Where(c => c.Id == calendarioId).Select(c => c.Observacoes).SingleOrDefaultAsync();

            return Ok(new { nota = nota });
        }

        [HttpGet]
        [Route("azure")]
        public IActionResult GetTesteAzure()
        {
            var cursos = "Invictus in Azure, baby!";

            return Ok(new { message = cursos });
        }

        [HttpGet]
        [Route("cursosUnidade")]
        public async Task<IEnumerable<TurmaViewModel>> GetCursosUnidade()
        {// validar p ntrazer turma do msm tipo se ele ja estiver matriculado tipo nao trazer cursos de nefermagem!
            //informar se nao tiver cursos em andamento
            // implementar para trazer outros cursos
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).SingleOrDefaultAsync();
            var cursos = await _queries.GetCursosUnidade(unidadeId);

            return cursos;
        }

        [HttpGet]
        [Route("cursosUnidadev2")]
        public async Task<IActionResult> GetCursosUnidadev2()
        {// validar p ntrazer turma do msm tipo se ele ja estiver matriculado tipo nao trazer cursos de nefermagem!
         //informar se nao tiver cursos em andamento
         // implementar para trazer outros cursos

            var pacote = await _context.TypePacotes.ToListAsync();// _queries.GetCursosUnidade(unidadeId);

            return Ok(pacote);// cursos;
        }



        [HttpGet]
        [Route("materias/{turmaId}/{profId}")]
        public async Task<IEnumerable<ProfessoresMateriaDto>> GetMaterias(int turmaId, int profId)
        {
            var materias = await _materiaQueries.GetMaterias(turmaId, profId);

            return materias;
        }

        [HttpGet]
        //[Route("")]
        public async Task<ActionResult> GetCursos()
        {
            var unidade = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            var results = await _queries.GetCursos(unidade);

            return Ok(results);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCursoById(int id)
        {
            var results = await _queries.GetCursoById(id);
            //var results = new List<CursoDto>();


            return Ok(results.FirstOrDefault());
        }

        [HttpGet]
        [Route("professoresturma/{turmaId}")]
        public async Task<IActionResult> GetProfessoresTurma(int turmaId)
        {
            //var results = await _queries.GetCursoById(id);
            var result = await _colaboradorQueries.GetProfessoresByTurmaId(turmaId);
            //var results = new List<CursoDto>();


            return Ok(result);
        }

        [HttpGet]
        [Route("pesquisa/{alunoId}")]
        public async Task<IActionResult> GetCursosAndamento(int alunoId, [FromQuery] string curso)
        {

            //var results = new List<CursoDto>();
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var matriculas = await _context.Matriculados.Where(m => m.AlunoId == alunoId).Select(m => m.TurmaId).ToListAsync();

            var idTurmasMatriculados = await _context.Turmas.Where(t => matriculas.Contains(t.Id)).ToListAsync();

            var results = await _queries.GetCursosAndamento(curso, idTurmasMatriculados.Select(t => t.PacoteId).ToArray(), unidadeId);
            // var typeIdsMatriculados = await _context.TypePacotes
            if (results.Count() == 0) return NotFound(new { message = "Não há turmas nesta unidade em que o aluno possa se matricular." });


            return Ok(results);
        }

        [HttpGet]
        [Route("pesquisav2/{typePacoteId}")]
        public async Task<IActionResult> GetCursosAndamentov2(int typePacoteId)
        {

            //var results = new List<CursoDto>();
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            //var matriculas = await _context.Matriculados.Where(m => m.AlunoId == alunoId).Select(m => m.TurmaId).ToListAsync();

            //var idTurmasMatriculados = await _context.Turmas.Where(t => matriculas.Contains(t.Id)).ToListAsync();
            var pacotesId = await _context.TypePacotes.Where(t => t.Id != typePacoteId).Select(t => t.Id).ToListAsync();//  new List<int>();// [{ typePacoteId}]
           // pacotesId.Add(typePacoteId);

            var results = await _queries.GetCursosAndamento(null, pacotesId.ToArray(), unidadeId);
            // var typeIdsMatriculados = await _context.TypePacotes
            if (results.Count() == 0) return NotFound(new { message = "Não há turmas nesta unidade em que o aluno possa se matricular." });


            return Ok(results);
        }

        [HttpGet]
        [Route("alunosturma")]
        public async Task<IActionResult> GetAlunosDaTurma([FromQuery] int turmaId)
        {
            var results = await _queries.GetAlunosDaTurma(turmaId);

            BindCPF(ref results);
            // TODO bind CPF

            return Ok(results);
        }

        [HttpGet]
        [Route("disponibilidade/{datetimeIni}/{datetimeEnd}/{salaId}")]
        public async Task<IActionResult> GetTeste(DateTime datetimeIni, DateTime datetimeEnd, int salaId)
        {
            // Thread.Sleep(2000);
            #region TESTE
            /*
            //teste

            var datasCalend = await _context.Calendarios.Where(c => c.DiaAula >= datetimeIni & c.DiaAula <= datetimeEnd).ToListAsync();
            var diaInicio = await _context.Calendarios.Where(c => c.DiaAula == datetimeIni).ToListAsync();

            var firtDay = Convert.ToInt32(datetimeIni.DayOfWeek); // primeiro dia: domingo = 0
            var diasSobra = 6 - firtDay;
            var datasParaPesquisa = new List<DateTime>();
            datasParaPesquisa.Add(datetimeIni);

            List<Tuple<DateTime, DateTime>> rangeDatas = new List<Tuple<DateTime, DateTime>>();
            for (int i = 0; i < 826; i++)
            {
                var dataParaAdd = datetimeIni.AddMinutes(i);
                datasParaPesquisa.Add(datetimeIni);
            }

            foreach (var item in datasParaPesquisa)
            {
                var datasCalendSear = datasCalend.Where(c => c.DiaAula.Hour >= 8 & c.DiaAula.Hour <= 13);
                // iff datasCalendSear = 0 significa que está livre todo o range! e faazer isso nos próximos ranges
                // fazer a pesquis sempre baseada NO PRIMEIRO DIA DE PESQUISA
            }

            int lasDayWek = Convert.ToInt32(datetimeIni.DayOfWeek); // segunda é 1
            */
            /*
            var dispoV2 = new DisponibilidadesViewModelV2();

            dispoV2.horarioDisponivelView = "Horários disponíveis: 08:00 às 22:30.";

            var dispoDayOne = new DisponibilidadeDayOnetoV2()
           // dispoDayOne.Add(new DisponibilidadeDtoV2()
            {
                diaSemana = datetimeIni.DayOfWeek.ToString(),
                horaIni = new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day, 8, 0, 0),
                horaFim = new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day, 22, 30, 0),
                diaData = new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day,0,0,0),
                
            };
            dispoV2.availabilityDayOne = dispoDayOne;
            var tuplas = new List<Tuple<DateTime, DateTime>>();
            tuplas.Add(new Tuple<DateTime, DateTime>(
                new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day, 8, 0, 0),
                new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day, 22, 30, 0)
                ));
            dispoDayOne.ranges.AddRange(tuplas);



            //dispoV2.availabilityDayOne.Add(dispoDayOne);

            var firtDay = Convert.ToInt32(datetimeIni.DayOfWeek); // primeiro dia: domingo = 0
            var diasSobra = 6 - firtDay;
            var datasParaPesquisa = new List<DateTime>();
            datasParaPesquisa.Add(datetimeIni);

            for (int i = 1; i <= diasSobra; i++)
            {
                var day = datetimeIni.AddDays(i);
                dispoV2.availabilityTwo.Add(new DisponibilidadeDay2DtoV2()
                {
                    diaSemana = day.DayOfWeek.ToString(),
                    diaData = day
                });
            }

            return Ok(dispoV2);

            */
            #endregion




            // OLD
            //DateTime novaData = Convert.ToDateTime(datetime);

            // TODO: IF FIRST OPEN COURSE!!!
            var unidadeId = await _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            var turmasQnt = await _context.Turmas.Where(t => t.UnidadeId == unidadeId).ToListAsync();



            //END TODO

            var pesquisaIni = new DateTime(datetimeIni.Year, datetimeIni.Month, datetimeIni.Day, 0, 0, 0);
            var diaInicial = pesquisaIni.DayOfWeek;
            int diasContados = DayOfWeek.Saturday - diaInicial;
            var pesquisaFinal = pesquisaIni.AddDays(diasContados);

            var calendarios = await _calendarioQueries.GetDatas(pesquisaIni, pesquisaFinal);

            var HaDiaDisponivelNaPesquisa = calendarios.Where(c => c.DiaAula == pesquisaIni);

            // if(HaDiaDisponivelNaPesquisa.Count() == 0) { return Ok(new { mensagem = "Escolha outro dia para início. Não há vagas para este dia!" });  }

            //var listaDisponibilidades = new List<DisponibilidadesDto>();
            var dias = new List<Dias>();

            var diasDistintos = calendarios.DistinctBy(c => c.DiaAula);
            //int quantidade = calendarios.Distinct().Count();

            foreach (var item in diasDistintos)
            {
                dias.Add(new Dias() { dia = item.DiaAula });
            }

            for (int i = 0; i < dias.Count(); i++)
            {
                foreach (var item in calendarios)
                {
                    if (dias[i].dia == item.DiaAula)
                    {
                        var calend = new CalendarioDto()
                        {
                            DiaAula = item.DiaAula,
                            Turno = item.Turno,
                            DiaDaSemana = item.DiaDaSemana,
                            Materia = "",
                            HoraInicial = "",
                            HoraFinal = "",
                            ProfessorId = "",
                            Turma = "",
                            Unidade = "",
                            Sala = ""
                        };
                        dias[i].calendarios.Add(calend);
                    }
                }
            }

            var vagas = new List<Vagas>();

            //var i = 0;
            // for (int i = 0; i < length; i++)


            //for (int i = 0; i < diasDistintos.Count(); i++) // MESMO DIA!!!!!!! dias é uma LISTA!!!!!!!!!!!
            //{   // manha: SO TEREI NO DIAS UMA MANHA OU UMA MANHA TARDE, MAS NAO UMA E OUTRA

            //    CultureInfo idiomaPT = new CultureInfo("pt-BR");
            //    var diaSemanaPT = diasDistintos.ToList()[i].DiaAula.ToString("dddd", idiomaPT);
            //    var diaSemanaDay = DayOfWeek.Saturday.ToString();



            //    //var diaSemanaEmPortugues = dia.ToString("dddd", idioma);


            //    //CultureInfo idiomaPT = new CultureInfo("pt-BR");
            //    //var semana = DayOfWeek.Saturday;
            //    //var diaSemanaPT = DateTimeFormatInfo.CurrentInfo.GetDayName(semana);

            //    var tuple = new Tuple<string, string>(diaSemanaPT, diaSemanaDay);
            //    //var i = dia.calendarios.Count(); //ex datas
            //    //manha
            //    //tenho quantas vagas de manha?
            //    var items1 = dia.calendarios.Where(c => c.Turno == Turno.manha.ToString() || c.Turno == Turno.IntegralManhaTarde.ToString());

            //    if (items1.Count() == 0)
            //    {
            //        vagas.Add(new Vagas() { turno = Turno.manha.ToString(), dia = dia.dia, turnoView = "Manhã" });
            //    }
            //    // se trouxer qualquer um acima, já nao tem mais vagas!!! Entao precisa vir ZERO pra confirma que tem uma vaga na manha
            //    // se tem uma vaga na manha, já basta pois não haverá DUAS vagas na manhã

            //    //tarde
            //    //tenho quantas vagas de tarde?
            //    var items2 = dia.calendarios.Where(c => c.Turno == Turno.tarde.ToString() || c.Turno == Turno.IntegralManhaTarde.ToString()
            //    || c.Turno == Turno.IntegralTardeNoite.ToString());

            //    if (items2.Count() == 0)
            //    {
            //        vagas.Add(new Vagas() { turno = Turno.tarde.ToString(), dia = dia.dia, turnoView = "Tarde" });
            //    }
            //    //tarde
            //    //tenho quantas vagas de noite?
            //    var items3 = dia.calendarios.Where(c => c.Turno == Turno.noite.ToString() || c.Turno == Turno.IntegralTardeNoite.ToString());

            //    if (items3.Count() == 0)
            //    {
            //        vagas.Add(new Vagas() { turno = Turno.noite.ToString(), dia = dia.dia, turnoView = "Noite" });
            //    }
            //}

            //var i = 0;
            foreach (var dia in dias) // MESMO DIA!!!!!!! dias é uma LISTA!!!!!!!!!!!
            {   // manha: SO TEREI NO DIAS UMA MANHA OU UMA MANHA TARDE, MAS NAO UMA E OUTRA


                //var i = dia.calendarios.Count(); //ex datas
                /// manha
                // tenho quantas vagas de manha?
                var items1 = dia.calendarios.Where(c => c.Turno == Turno.manha.ToString() || c.Turno == Turno.IntegralManhaTarde.ToString());

                if (items1.Count() == 0)
                {
                    vagas.Add(new Vagas() { turno = Turno.manha.ToString(), dia = dia.dia, turnoView = "Manhã" });
                }
                //se trouxer qualquer um acima, já nao tem mais vagas!!! Entao precisa vir ZERO pra confirma que tem uma vaga na manha
                // se tem uma vaga na manha, já basta pois não haverá DUAS vagas na manhã

                // tarde
                // tenho quantas vagas de tarde?
                var items2 = dia.calendarios.Where(c => c.Turno == Turno.tarde.ToString() || c.Turno == Turno.IntegralManhaTarde.ToString()
                || c.Turno == Turno.IntegralTardeNoite.ToString());

                if (items2.Count() == 0)
                {
                    vagas.Add(new Vagas() { turno = Turno.tarde.ToString(), dia = dia.dia, turnoView = "Tarde" });
                }
                // tarde
                //tenho quantas vagas de noite?
                var items3 = dia.calendarios.Where(c => c.Turno == Turno.noite.ToString() || c.Turno == Turno.IntegralTardeNoite.ToString());

                if (items3.Count() == 0)
                {
                    vagas.Add(new Vagas() { turno = Turno.noite.ToString(), dia = dia.dia, turnoView = "Noite" });
                }
            }

            CultureInfo idioma = new CultureInfo("pt-BR");
            foreach (var item in vagas)
            {
                item.diaDaSemanaPesquisaView = pesquisaIni.ToString("dddd", idioma);
                item.diaDaSemanaPesquisa = pesquisaIni.ToString("dddd");
            }

            var vagasDistinct = vagas.DistinctBy(d => d.dia);

            //var diaSemana = data.DayOfWeek;
            var view = new CreateTurmaViewModel();
            // v 1
            //var turno1 = new TurnosViewModel();
            //var turno2 = new TurnosViewModel();
            //turno1.turno = "Manhã";
            //turno1.diaSemana = "2º feira";
            //turno1.diasSemanaDisponiveis.Add("4ª feira");
            //turno1.diasSemanaDisponiveis.Add("5ª feira");

            //turno2.turno = "Tarde";
            //turno2.diaSemana = "2º feira";
            //turno2.diasSemanaDisponiveis.Add("3ª feira");

            //view.turnos.Add(turno1);
            //view.turnos.Add(turno2);


            // 2 
            var turnos = new List<TurnosViewModel>();

            var turno1 = new TurnosViewModel();
            turno1.turno = "Manhã";
            turno1.primeiroDiaSemana = new Tuple<string, string>("3ª Feira", "Tuesday");
            var tupla1 = new Tuple<string, string>("4ª Feira", "Wednesday");
            var tupla2 = new Tuple<string, string>("5ª Feira", "Thursday");
            turno1.diasSemanaDisponiveis.Add(tupla1);
            turno1.diasSemanaDisponiveis.Add(tupla2);

            var turno2 = new TurnosViewModel();
            turno2.turno = "Tarde";
            turno2.primeiroDiaSemana = new Tuple<string, string>("3ª Feira", "Tuesday");
            var tupla3 = new Tuple<string, string>("3ª Feira", "Tuesday");
            turno2.diasSemanaDisponiveis.Add(tupla3);

            turnos.Add(turno1);
            turnos.Add(turno2);

            // teste 3
            // datetime
            CultureInfo idioma2 = new CultureInfo("pt-BR");
            DateTime dia2 = datetimeIni;
            var diaSemanaEmPortugues2 = dia2.ToString("dddd", idioma2);
            var disponibilidades = new DisponibilidadesViewModel();
            //primeiro dia 
            //horarios disponíveis: 08:00 às 12:30 
            var dispo1 = new DisponibilidadeDto();
            dispo1.horarioDisponivelView = "horarios disponíveis: 08:00 às 22:30";
            dispo1.diaSemana = dia2.DayOfWeek.ToString();
            dispo1.diaSemanaView = diaSemanaEmPortugues2;
            var horarios = new List<HorariosViewDto>();
            horarios.Add(new HorariosViewDto() { inicio = "08:00", fim = "22:30" });
            dispo1.horarios = horarios;

            disponibilidades.availabilityOne = dispo1;

            if (datetimeIni.DayOfWeek == DayOfWeek.Sunday || datetimeIni.DayOfWeek == DayOfWeek.Saturday)
            {
                disponibilidades.availabilityTwo = null;
            }
            else
            {
                if ((datetimeIni.DayOfWeek + 1) == DayOfWeek.Saturday)
                {
                    disponibilidades.availabilityTwo = null;
                }
                else
                {
                    var diadoisdisponivel = new DisponibilidadeDto();

                    diadoisdisponivel.horarioDisponivelView = "horarios disponíveis: 08:00 às 22:30";
                    //var diaSemanaEmPortugues2 = dia2.ToString("dddd", idioma2);
                    diadoisdisponivel.diaSemana = (dia2.DayOfWeek + 1).ToString(); //diaSemanaEmPortugues2 + 1;
                    var dia3 = dia2.AddDays(1);
                    diadoisdisponivel.diaData = dia3;
                    diadoisdisponivel.diaSemanaView = dia3.ToString("dddd", idioma2);
                    var horarios2 = new List<HorariosViewDto>();
                    horarios2.Add(new HorariosViewDto() { inicio = "08:00", fim = "22:30" });
                    diadoisdisponivel.horarios = horarios2;

                    disponibilidades.availabilityTwo.Add(diadoisdisponivel);

                }



            }


            return Ok(disponibilidades);
            //return Ok(vagas);
        }

        [HttpGet]
        [Route("agenda")]
        public async Task<IActionResult> GetAgendas([FromQuery] int turmaId, [FromQuery] int avaliacao)
        {
            var agendas = await _turmaQueries.GetAgendas(turmaId, avaliacao);

            return Ok(agendas.OrderBy(m => m.materiaId));
        }


        #endregion

        #region POSTs

        [HttpPost]
        public IActionResult SaveCurso([FromBody] CreateCursoDto newTurma)
        {
            // TODO: validar TUDO, sala,, horarios etc..se da p salvar no dia
            // temp:
            var unidadeUsuario = _userHttpContext.HttpContext.User.FindFirst("Unidade").Value;
            var unidade = _context.Unidades.Where(u => u.Sigla == unidadeUsuario).SingleOrDefault();

            //var unidade = unidadeUsuario;// _context.Unidades "CGI";// Unidades.CGI;

            var turma = new Turma();
            turma.SetPlanoPagamento(newTurma.planoId);
            var turmasExistetes = _queries.GetQuantidadeTurma(unidade.Id).GetAwaiter().GetResult();

            //Turno turno;
            //Enum.TryParse(newCurso.turno, out turno);
            // turma.GerarIdentificador(unidade, turmasExistetes);

            //var modulo = _mapper.Map<ModuloDto, Modulo>(newTurma.modulo);
            var modulo = _context.Modulos.Where(m => m.Id == newTurma.modulo).FirstOrDefault(); // na verdade pacote
            var sala = _context.Salas.Find(newTurma.salaId);
            // abaixo: cria calendario, previsoes, horarios
            //turma.Factory(modulo, newTurma.vagas, newTurma.minVagas, unidadeId, unidadeUsuario, turmasExistetes, newTurma.prevInicio_1, newTurma.prevInicio_2, newTurma.prevInicio_3,
            //    newTurma.horarios.dia1, newTurma.horarios.dia2, newTurma.horarios.horarioIni_1, newTurma.horarios.horarioFim_1, newTurma.salaId);
            turma.FactoryInitialValuesOne(unidade, turmasExistetes, sala, newTurma.minVagas, modulo.TypePacoteId);

            turma.FactoryInitialValuesTwo(modulo,
                newTurma.prevInicio_1, newTurma.prevInicio_2, newTurma.prevInicio_3,
                newTurma.prevTermino_1, newTurma.prevTermino_2, newTurma.prevTermino_3,
                newTurma.descricao);

            var diaSemanaUm = DiaDaSemana.TryParse(newTurma.prevInicio_1.DayOfWeek);
            var diaSemanaDois = DiaDaSemana.TryParse(newTurma.segundoDiaAula.DayOfWeek);

            var feriados = _context.ParametrosValues.Where(p => p.ParametrosTypeId == 1).Select(p => p.Nome).ToList();

            // n
            var datas = new List<DataFeriado>();
            foreach (var item in feriados)
            {
                var diaMes = item.Split('/');
                var data = new DataFeriado();
                data.dia = Convert.ToInt32(diaMes[0]);
                data.mes = Convert.ToInt32(diaMes[1]);
                datas.Add(data);
            }

            turma.CreateCalendarioDaTurma(
               newTurma.prevInicio_1, newTurma.prevTermino_3,
               //newTurma.prevInicio_1.DayOfWeek.ToString(), newTurma.segundoDiaAula.DayOfWeek.ToString(),
               diaSemanaUm.DisplayName, diaSemanaDois.DisplayName,
               newTurma.horarioIni_1, newTurma.horarioFim_1,
               newTurma.horarioIni_2, newTurma.horarioFim_2,
               datas);

            // set materias na turma

            var mats = _context.Materias.Where(m => m.ModuloId == modulo.Id & m.Modalidade == "Presencial").ToList();
            var matsPrimeiroDia = mats.Where(m => m.PrimeiroDiaAula == "Sim" || m.PrimeiroDiaAula == "Ambos");
            var matsSegundoDia = mats.Where(m => m.PrimeiroDiaAula == "Nao" || m.PrimeiroDiaAula == "Ambos");


            turma.CreateHorariosDaTurma(newTurma.prevInicio_1, newTurma.prevInicio_1.DayOfWeek.ToString(),
                null, newTurma.horarioIni_1, newTurma.horarioFim_1);

            _turmaRepository.AddTurma(turma);

            #region SET MATS IN CALENDARIO


            var turmasPrimeiroDia = _context.Calendarios.Where(t => t.TurmaId == turma.Id).OrderBy(t => t.DiaAula).ToList(); // 188 aulas sem contar sexta feira

            var index = 0;
            foreach (var item in mats.OrderBy(m => m.Semestre))
            {
                for (int i = 0; i < item.QntAulas; i++)
                {
                    turmasPrimeiroDia[index].SetMateriaId(item.Id);
                    index++;
                }

            }

            Debug.WriteLine("OK");
            _context.Calendarios.UpdateRange(turmasPrimeiroDia);
            
            //OLD
            


            // OLD 

            // PRIMEIRO DIA
            //var turmasPrimeiroDia = _context.Calendarios.Where(t => t.DiaDaSemana == diaSemanaUm.DisplayName & t.TurmaId == turma.Id).OrderBy(t => t.DiaAula).ToList();

            //var matCalOne = new List<insertCalendarMat>();
            //foreach (var mat in matsPrimeiroDia.OrderBy(m => m.Semestre))
            //{
            //    for (int i = 0; i < mat.QntAulas; i++)
            //    {
            //        matCalOne.Add(new insertCalendarMat(mat.Id));
            //    }
            //}
            //var index = 0;
            //foreach (var item in matCalOne)
            //{
            //    if (index == turmasPrimeiroDia.Count()) break;
            //    turmasPrimeiroDia[index].SetMateriaId(item.Id);
            //    index++;
            //}

            //_context.Calendarios.UpdateRange(turmasPrimeiroDia);
            //_context.SaveChanges();
            //// Segundo DIA
            //var turmasSegundoDia = _context.Calendarios.Where(t => t.DiaDaSemana == diaSemanaDois.DisplayName & t.TurmaId == turma.Id).OrderBy(t => t.DiaAula).ToList();
            //var matCalTwo = new List<insertCalendarMat>();
            //foreach (var mat in matsSegundoDia.OrderBy(m => m.Semestre))
            //{
            //    for (int i = 0; i < mat.QntAulas; i++)
            //    {
            //        matCalTwo.Add(new insertCalendarMat(mat.Id));
            //    }
            //}
            //var indexb = 0;
            //foreach (var item in matCalTwo)
            //{
            //    if (indexb == turmasSegundoDia.Count()) break;
            //    turmasSegundoDia[indexb].SetMateriaId(item.Id);
            //    indexb++;
            //}
            //_context.Calendarios.UpdateRange(turmasSegundoDia);
            //_context.SaveChanges();

            #endregion

            #region refact
            // criar apenas ao iniciar turma

            var turmaPedag = new TurmaPedagogico();

            turmaPedag.CreateTurmaPedagogico(turma.Id, modulo.Id);


            _turmaPedagRepository.CreateTurmaPedag(turmaPedag);


            var materias = _unidadeQueries.GetMaterias(modulo.Id).GetAwaiter().GetResult();
            var listaPedagMAterias = new List<MateriaPedag>();

            foreach (var item in materias)
            {
                listaPedagMAterias.Add(new MateriaPedag(0, item.descricao, item.id, turmaPedag.Id));
            }

            _turmaPedagRepository.CreateMateriasPedag(listaPedagMAterias);

            #endregion

            // Create ProvasAgenda

            var agendaProvas = new List<ProvasAgenda>();

            foreach (var mat in materias)
            {
                var agenda = new ProvasAgenda();
                agenda.Factory(turma.Id, mat.id, mat.descricao);
                agendaProvas.Add(agenda);
            }

            _context.ProvasAgenda.AddRange(agendaProvas);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        [Route("turma")]
        public IActionResult AddAlunoTurma(
            [FromBody] SubmitMatriculaForm form
            /*[FromQuery] int idAluno, [FromQuery] int idTurma, [FromQuery] string ciencia, [FromQuery] string meioPagamento, [FromQuery] string parcelas*/)
        {

            //var alunoTurma = new AlunosTurma(0, idAluno, idTurma);
            //var aluno
            //var aluno = _turmaPedagRepository.AddAlunoTurma(idAluno, idTurma, ciencia);

            // vendo se aluno ja está na turma
            var turmaSearch = _context.Turmas.Where(t => t.Id == form.idTurma).FirstOrDefault();

            var livroMatricula = _context.Matriculados.Where(l => l.TurmaId == form.idTurma & l.AlunoId == form.idAluno);

            if (livroMatricula.Count() != 0) return BadRequest();

            // add aluno na matricula
            var aluno = _context.Alunos.Find(form.idAluno);
            var matriculado = new Matriculados(0, aluno.Id, aluno.Nome, aluno.CPF, "Matriculado", form.idTurma);
            matriculado.SetDiaMatricula();
            var totalAlunosInDataBase = _context.Alunos.Count();
            matriculado.SetNumeroMatricula(totalAlunosInDataBase);
            _context.Matriculados.Add(matriculado);
            //_context.SaveChanges();

            // gerando boletim escolar
            var boletim = new HistoricoEscolar();
            var turma = _context.Turmas.Find(form.idTurma);
            turma.AddAlunoNaTurma();
            _context.Turmas.Update(turma);
            // _context.SaveChanges();
            var disciplinas = _unidadeQueries.GetMateriasDoCurso(turma.ModuloId).GetAwaiter().GetResult();
            boletim.CreateBoletimEscolar(aluno.Nome, form.idAluno, form.idTurma, disciplinas);
            _historicoEscolarRepo.CreateHistoricoEscolar(boletim);

            // gerando lista notas e disciplinas
            var notas = new List<NotasDisciplinas>();
            var materias = _context.Materias.Where(m => m.ModuloId == turma.ModuloId).ToList();
            var notasDisc = new NotasDisciplinas();
            notas = notasDisc.CreateNotasDisciplinas(materias, form.idAluno, form.idTurma);

            _context.NotasDisciplinas.AddRange(notas);
            //_context.SaveChanges();
            // todo: crate BolteimNotasVazio
            //_cursoRepository.AddAlunoTurma(alunoTurma, idTurma, ciencia);

            // criar histórico escolar
            // add aluno no livro matricula

            //int duracaoMeses = 20;

            //// GERAR OS DOCSSSS!!!!!!!!!!!!!!!!!!!!!!!
            var docsExigenciaAluno = _context.DocsExigencias.Where(
                d => d.ModuloId == turma.ModuloId & 
                d.Titular == "Aluno").ToList();

            var docsAluno = new List<DocumentoAluno>();
            if (docsExigenciaAluno.Count() > 0)
            {


                foreach (var item in docsExigenciaAluno)
                {
                    docsAluno.Add(new DocumentoAluno(aluno.Id, item.Descricao, item.Comentario,
                        false, false, false, turma.Id));
                }

            }
            _context.DocumentosAlunos.AddRange(docsAluno);


            

            var docsRespMenor = new List<DocumentoAluno>();
            if (aluno.TemRespMenor == true)
            {
                var docsExigenciaRespMenor = _context.DocsExigencias.Where(
                d => d.ModuloId == turma.ModuloId &
                d.Titular == "Responsável menor").ToList();

                foreach (var item in docsExigenciaRespMenor)
                {
                    docsRespMenor.Add(new DocumentoAluno(aluno.Id, item.Descricao, item.Comentario,
                        false, false, false, turma.Id));
                }

                _context.DocumentosAlunos.AddRange(docsRespMenor);

            }


            var docsRespFin = new List<DocumentoAluno>();
            if (aluno.TemRespFin == true)
            {
                var docsExigenciaRespFin = _context.DocsExigencias.Where(
                d => d.ModuloId == turma.ModuloId &
                d.Titular == "Responsável financeiro").ToList();

                foreach (var item in docsExigenciaRespFin)
                {
                    docsRespFin.Add(new DocumentoAluno(aluno.Id, item.Descricao, item.Comentario,
                        false, false, false, turma.Id));
                }

                _context.DocumentosAlunos.AddRange(docsRespFin);

            }

            _context.SaveChanges();

            #region Financeiro


            var unidadeId = _context.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefault();
            var infoFinanc = new InformacaoFinanceiraAggregate();
            if (form.meioPagamento == "dinheiro" || form.meioPagamento == "pix" || form.meioPagamento == "debito")
            {
                infoFinanc = new InformacaoFinanceiraAggregate(0, form.idAluno, form.idTurma, 4290, DateTime.Now, unidadeId, 1, true);
            }
            else if (form.meioPagamento == "credito")
            {
                infoFinanc = new InformacaoFinanceiraAggregate(0, form.idAluno, form.idTurma, 4290, DateTime.Now, unidadeId, Convert.ToInt32(form.parcelas), true);
            }
            else if (form.meioPagamento == "boleto")
            {
                infoFinanc = new InformacaoFinanceiraAggregate(0, form.idAluno, form.idTurma, 4290, DateTime.Now, unidadeId, Convert.ToInt32(form.parcelas), false);
            }


            // LISTA DÉBITOS

            var meioPagamento = MeioPagamento.TryParse(form.meioPagamento);
            // IF BOLETO
            // TODO primeira parcela com data de pagamento atual em virtude da primeira parcela
            var debitos = new List<Debito>();
            if (form.meioPagamento == "dinheiro" || form.meioPagamento == "pix" || form.meioPagamento == "debito")
            {
                debitos.Add(new Debito(0, DateTime.Now, unidadeId, StatusPagamento.Pago, 4290, DateTime.Now, meioPagamento, 1, 1, "Curso Técnico em Enfermagem", "Pagamento a vista"));

            }
            else if (form.meioPagamento == "credito")
            {

                var nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var comentario = "";
                if (form.parcelas == "vista")
                {

                    nowMont = nowMont.AddDays(30);// new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    comentario = "Pagamento a vista";// Parcela " + Convert.ToString(form.parcelas) + "x";
                    debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.Pago, 4290, nowMont, meioPagamento, 1, 0, "Curso Técnico em Enfermagem", comentario));
                }
                else
                {
                    decimal valorParcelas = 4290 / Convert.ToInt32(form.parcelas);
                    //var nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    nowMont = nowMont.AddDays(30);// new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    comentario = "Pagamento parcelado 1/" + form.parcelas;// Parcela " + Convert.ToString(form.parcelas) + "x";
                    debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.Pago, valorParcelas, nowMont, meioPagamento, Convert.ToInt32(form.parcelas), 0, "Curso Técnico em Enfermagem", comentario));

                    for (int i = 2; i <= Convert.ToInt32(form.parcelas); i++)
                    {
                        nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);//     DateTime.Now.AddMonths(i + 1);
                        nowMont = nowMont.AddDays(30 * i);// .AddMonths(i + 1);
                        comentario = "Pagamento parcelado " + Convert.ToString(i) + "/" + form.parcelas;// Parcela " + Convert.ToString(form.parcelas) + "x";
                        debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.Pago, valorParcelas, nowMont, meioPagamento, Convert.ToInt32(form.parcelas), 0, "Curso Técnico em Enfermagem", comentario));
                    }
                }
            }
            else // BOLETO
            {
                var nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var comentario = "";
                if (form.parcelas == "vista")
                {
                    //nowMont = nowMont.AddDays(30);// new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    comentario = "Boleto a vista";// Parcela " + Convert.ToString(form.parcelas) + "x";
                    debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.EmAberto, 4290, nowMont, meioPagamento, 1, 0, "Curso Técnico em Enfermagem", comentario));
                }
                else
                {
                    decimal valorParcelas = 4290 / Convert.ToInt32(form.parcelas);
                    nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);


                    //nowMont = nowMont.AddDays(30);// new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    comentario = "Boleto 1/" + form.parcelas;// Parcela " + Convert.ToString(form.parcelas) + "x";
                    debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.EmAberto, valorParcelas, nowMont, meioPagamento, Convert.ToInt32(form.parcelas), 0, "Curso Técnico em Enfermagem", comentario));



                    for (int i = 1; i < Convert.ToInt32(form.parcelas); i++)
                    {
                        nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10, 0, 0, 0);

                        nowMont = nowMont.AddMonths(i);// new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);

                        //var nowMont = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(form.diaVencimento), 0, 0, 0);//     DateTime.Now.AddMonths(i + 1);
                        //nowMont = nowMont.AddMonths(i);// .AddMonths(i + 1);
                        comentario = "Boleto " + Convert.ToString(i) + "/" + form.parcelas;// Parcela " + Convert.ToString(form.parcelas) + "x";
                        debitos.Add(new Debito(0, nowMont, unidadeId, StatusPagamento.EmAberto, valorParcelas, nowMont, meioPagamento, Convert.ToInt32(form.parcelas), 0, "Curso Técnico em Enfermagem", comentario));
                    }
                }
            }

            // GERAR BOLETOS
            var boletos = new List<BoletoLoteDto>();
            var index = 0;
            foreach (var item in debitos)
            {
                index++;
                var boletoDto = new BoletoLoteDto();
                boletoDto.vencimento = item.DataVencimento.ToString("MM/dd/yyyy");
                boletoDto.valor = item.ValorTitulo.ToString();
                boletoDto.juros = "0";
                boletoDto.juros_fixo = "0";
                boletoDto.multa = "0";
                boletoDto.multa_fixo = "0";
                boletoDto.desconto = "";
                boletoDto.diasdesconto1 = "";
                boletoDto.desconto2 = "";
                boletoDto.diasdesconto2 = "";
                boletoDto.desconto3 = "";
                boletoDto.diasdesconto3 = "";
                boletoDto.nunca_atualizar_boleto = "0";

                boletoDto.nome_cliente = aluno.Nome;
                boletoDto.telefone_cliente = "";
                boletoDto.cpf_cliente = aluno.CPF;
                boletoDto.endereco_cliente = aluno.Logradouro;
                boletoDto.complemento_cliente = "";
                boletoDto.bairro_cliente = aluno.Bairro;
                boletoDto.cidade_cliente = aluno.Cidade;
                boletoDto.estado_cliente = aluno.UF;
                boletoDto.cep_cliente = aluno.CEP;

                boletoDto.logo_url = "";
                boletoDto.texto = "Curso Invictus";
                boletoDto.instrucoes = "";
                boletoDto.instrucao_adicional = "";
                boletoDto.grupo = "Boletos" + debitos.Count();
                boletoDto.webhook = "";
                boletoDto.pedido_numero = "000" + index;
                //boletoDto.especie_documento = "";
                boletoDto.pix = "pix-e-boleto";

                boletos.Add(boletoDto);
            }

            var boletosResp = _boletoService.EnviaRequisicaoLote(boletos);

            for (int ix = 0; ix < boletosResp.Count(); ix++)
            {
                var bole = _mapper.Map<Boleto>(boletosResp[ix]);
                _context.Boletos.Add(bole);
                _context.SaveChanges();
                debitos[ix].SetBoletoId(Convert.ToInt32(bole.Id));

            }



            infoFinanc.Debitos.AddRange(debitos);
            _context.InfoFinanceiras.Add(infoFinanc);

            var vendaCurso = new VendaCursoAggregate(0, DateTime.Now, 4290, 0,
                unidadeId, "TODO", 0, meioPagamento, "xxxxxxxxxxxxxxxx", Convert.ToInt32(form.parcelas));

            var cursoVenda = new CursoVenda(0, form.idTurma, 1, 4290, 4290);

            vendaCurso.CursosVendas.Add(cursoVenda);

            _context.VendasCursos.Add(vendaCurso);
            _context.SaveChanges();

            #endregion

            return Ok();
        }


        public void BindCPF(ref List<AlunoDto> alunos)
        {
            foreach (var item in alunos)
            {

                string substr = item.cpf.Substring(6, 3);
                item.cpf = "******." + substr + "-**";
            }

        }


        [HttpPost]
        [Route("Professores")]
        public IActionResult SaveProfsInTurma([FromBody] SaveProfsCommand professores)
        {

            _turmaPedagRepository.AddProfInTurma(professores.listProfsIds.ToList(), professores.turmaId);
            //List<Tuple<int, int, int>> professoresTurma = new List<Tuple<int, int, int>>();

            //foreach (var item in professores.listProfsIds)
            //{
            //    professoresTurma.Add(new Tuple<int, int, int>(0, item, professores.turmaId));
            //}

            //_cursoRepository.AddProfsCurso(professoresTurma);

            return Ok();
        }


        [HttpPost]
        [Route("agenda-atualizar/{avaliacao}")]
        public IActionResult EditAgenda([FromBody] List<AgendasProvasDto> agendasDto, int avaliacao)
        {

            foreach (var agendaDto in agendasDto)
            {
                var agenda = _context.ProvasAgenda.Find(agendaDto.id);

                if (avaliacao == 1)
                {
                    agenda.UpdateAvOne(agendaDto.avaliacaoUm, agendaDto.segundaChamadaAvaliacaoUm);

                }
                else if (avaliacao == 2)
                {
                    agenda.UpdateAvTwo(agendaDto.avaliacaoDois, agendaDto.segundaChamadaAvaliacaoDois);
                }
                else if (avaliacao == 3)
                {
                    agenda.UpdateAvTree(agendaDto.avaliacaoTres, agendaDto.segundaChamadaAvaliacaoTres);
                }

                _context.Update(agenda);
                _context.SaveChanges();
            }


            return Ok();
        }

        #endregion

        #region PUTs


        [HttpPut]
        [Route("calendario/{calendarioId}")]
        public IActionResult IniciarAula(int calendarioId)
        {
            
            var calendario = _context.Calendarios.Find(calendarioId);

            calendario.IniciarAula();
            calendario.SetDataInicioAula();
            _context.Calendarios.Update(calendario);

            _context.SaveChanges();
           
            return Ok();
        }


        [HttpPut]
        [Route("turma/{turmaId}")]
        public IActionResult IniciarTurma(int turmaId)
        {
            // TODO: SETAR DATA A PARTIR DA PRÓXIMO DISPONÍVEL
            _cursoRepository.IniciarTurma(turmaId);

            //_agendaRepository.CreateScheduleProof(turmaId);


            //var turma = _context.Turmas.Find(turmaId);

            //var turmaPedag = new TurmaPedagogico();

            //turmaPedag.CreateTurmaPedagogico(turmaId, turma.ModuloId);


            //_turmaPedagRepository.CreateTurmaPedag(turmaPedag);


            //var materias = _unidadeQueries.GetMaterias(turma.ModuloId).GetAwaiter().GetResult();
            //var listaPedagMAterias = new List<MateriaPedag>();

            //foreach (var item in materias)
            //{
            //    listaPedagMAterias.Add(new MateriaPedag(0, item.Descricao, item.Id, turmaPedag.Id));
            //}

            //_turmaPedagRepository.CreateMateriasPedag(listaPedagMAterias);

            //_turmaRepository.UpdateProfessoresTurma(profsMaterias);
            // TODO: normalize calendario a partir da data nova
            return Ok();
        }

        [HttpPut]
        [Route("turma/adiar/{turmaId}")]
        public IActionResult AdiarInicio(int turmaId)
        {
            var previsao = _turmaQueries.GetPrevisaoAtual(turmaId).GetAwaiter().GetResult();
            if (previsao == "3ª previsão") return Ok(new { message = "Não é possível mais adiar!" });
            // TODO: SETAR DATA A PARTIR DA PRÓXIMO DISPONÍVEL
            var turma = new Turma();
            //turma.CalendarioFactory()

            _cursoRepository.AdiarInicio(turmaId);


            //_turmaRepository.UpdateProfessoresTurma(profsMaterias);
            // TODO: normalize calendario a partir da data nova
            return Ok();
        }

        [HttpPut]
        [Route("materias/{turmaId}/{profId}")]
        public IActionResult SetMateriaProfessor([FromBody] IEnumerable<ProfessoresMateriaDto> professoresMaterias, int turmaId, int profId)
        {
            var prof = _context.ProfessoresNew.Where(c => c.TurmaId == turmaId & c.ProfId == profId).SingleOrDefault();

            var materias = _context.MateriasDaTurma.Where(t => t.ProfessorId == prof.Id);

            if (materias.Count() > 0)
            {
                _context.MateriasDaTurma.Where(m => m.ProfessorId == prof.Id).DeleteFromQuery();
                _context.SaveChanges();
            }

            var temParaAdd = professoresMaterias.Where(m => m.temProfessor == true);

            var ids = professoresMaterias.Select(c => c.materiaId);
            var materiasTurma = new List<MateriasDaTurma>();
            if (temParaAdd.Count() > 0)
            {

                foreach (var item in temParaAdd)
                {
                    materiasTurma.Add(new MateriasDaTurma(0, item.descricao, item.materiaId, item.profId, prof.Id));

                }

                _context.MateriasDaTurma.AddRange(materiasTurma);
                _context.SaveChanges();

            }

            var materiasIds = professoresMaterias.Where(p => p.temProfessor == true).Select(p => p.materiaId).ToList();

            foreach (var item in materiasIds)
            {
                var calendarios = _context.Calendarios.Where(c => c.TurmaId == turmaId).ToList();

                foreach (var cal in calendarios.Where(c => c.MateriaId == item))
                {
                    cal.SetProfessorId(profId);

                }

                _context.UpdateRange(calendarios);
                _context.SaveChanges();
            }
            
            // pegar a lista de Id das materias onde for TRUE na var professoresMaterias, o profId e a turma Id
            // buscar pela turma com esses parans e setar o prof no dia das aulas la

            // _context.MateriasDaTurma.Remove()
            //var profsMaterias = _mapper.Map<IEnumerable<ProfessoresMateriaDto>, IEnumerable<ProfessoresMateria>>(professoresMaterias);

            // _turmaRepository.UpdateProfessoresTurma(profsMaterias);

            return Ok();
        }

        #endregion

        #region DELETE

        [HttpDelete]
        [Route("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        // [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Delete(int id)
        {
            //_adminAppService.RemoveGenericTask(genericTaskId);
            _cursoRepository.DeleteCurso(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("excluir/{profId}/{turmaId}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        // [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ExcluirProfessorDaTurma(int profId, int turmaId)
        {
            //_adminAppService.RemoveGenericTask(genericTaskId);
            _cursoRepository.ExcluirProfessorTurma(profId, turmaId);
            return NoContent();
        }

        #endregion
    }

    

    //public class SubmitMatriculaForm
    //{
    //    public int idAluno { get; set; }
    //    public int idTurma { get; set; }
    //    public string ciencia { get; set; }
    //    public string meioPagamento { get; set; }
    //    public string parcelas { get; set; }
    //    public int percentualDesconto { get; set; }
    //    public bool primeiraParceJaPaga { get; set; }
    //    public string diaVencimento { get; set; }
    //    //public bool aVista { get; set; }
    //}

    public class insertCalendarMat
    {
        public insertCalendarMat(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

}

