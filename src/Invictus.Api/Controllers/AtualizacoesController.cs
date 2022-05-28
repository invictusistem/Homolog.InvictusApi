using AutoMapper;
using Invictus.Application.Extensions;
using Invictus.Core;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/atualizar")]
    public class AtualizacoesController : ControllerBase
    {
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly ICalendarioRepo _calendarioRepo;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IPacoteQueries _pacoteQueries;
        private readonly IParametrosQueries _paramQueries;
        private readonly IMapper _mapper;
        private readonly ITurmaRepo _turmaRepo;
        private readonly InvictusDbContext _db;
        //private readonly ITurmaNotasRepo _notasRepo;

        public AtualizacoesController(IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser, ITurmaQueries turmaQueries,
            IPacoteQueries pacoteQueries, IMapper mapper, ITurmaRepo turmaRepo, IParametrosQueries paramQueries,
            ICalendarioRepo calendarioRepo, InvictusDbContext db)
        {
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
            _turmaQueries = turmaQueries;
            _pacoteQueries = pacoteQueries;
            _mapper = mapper;
            _turmaRepo = turmaRepo;
            _paramQueries = paramQueries;
            _calendarioRepo = calendarioRepo;
            _db = db;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            // verificar status/dia da turma e reiniciar

            // verificar boletos vencidos e mudar status

            // transferir, perguntar se tem q pagar algo e se vai esperar confirmação só apos pagar
            //entao reserva vaga do aluno na turma via log e qnd paga atualiza e libera transferencia

            // verificar pagamento confirmação de  matricula

            /// TUDO loggs do q fez (log do dia) salvar json? melhor talvez
            /// 
            //procurar documento vencido

            // habilitar edição de notas dos alunos depois da última aula

            // Docs Vencidos

            // verificar no TurmasPresencas se tem calendario de aula passada que nao foi gerada ListaDePresenca e gerar !?

            // atualizar lista notas diariamente !?

            // ver a turma e se todas as aulas já foram dadas e notas lançadas para mudar status para concluído

            // criar os dados da Tabela TurmasPresencas diariamente

            // Ao criar app. Unidade, criar a caixa da escola

            // colocar estagio em andamento se a data inicio for "hoje"

            return Ok();
        }

        [HttpGet]
        [Route("calendario/{turmaId}/{pacoteId}/{salaId}")]
        public async Task<IActionResult> GerarCalendarios(Guid turmaId, Guid pacoteId, Guid salaId)
        {
            var turmaSalvaId = turmaId;// new Guid("2a42c6f2-17b3-4727-96bd-35ede505e445");
            //var diasQnt = command.diasSemana.Count();
            var diasSemana = new List<DiasSemanaCommand>();

            var turmaHorarios = _db.Horarios.Where(h => h.TurmaId == turmaId).ToList();

            foreach (var horario in turmaHorarios)
            {
                diasSemana.Add(new DiasSemanaCommand()
                {
                    diaSemana = horario.DiaSemanada,
                    horarioInicio = horario.HorarioInicio,
                    horarioFim = horario.HorarioFim
                });
            }

            
            var command = new CreateTurmaCommand();

            command.descricao = "ENFERMAGEM 01";
            command.diasSemana = diasSemana;
            command.minVagas = 30;
            command.pacoteId = pacoteId;// new Guid("3b1f2590-897f-4107-80fa-08d9af7ea64a");
            command.prevInicio_1 = new DateTime(2022,01,15);
            command.prevInicio_2 = new DateTime(2022,01,22);
            command.prevInicio_3 = new DateTime(2022,01,29);
            command.prevTermino_1 = new DateTime(2023,12,16);
            command.prevTermino_2 = new DateTime(2023,12,19);
            command.prevTermino_3 = new DateTime(2024,5,20);
            command.salaId = salaId;//new Guid("614ba928-17de-47f3-ebfc-08d9af7f3f83");



            var sala = await _unidadeQueries.GetSala(command.salaId);
            var siglaUnidade = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(siglaUnidade);
            var totalTurmas = await _turmaQueries.CountTurmas(unidade.id);
            var typePacote = await _pacoteQueries.GetPacoteById(command.pacoteId);
            var materias = await _turmaQueries.GetMateriasFromPacotesMaterias(command.pacoteId);
            var turmaMaterias = _mapper.Map<IEnumerable<TurmaMaterias>>(materias);



            var previsao = new Previsao(command.prevInicio_1, command.prevTermino_1, "1ª previsão", DateTime.Now);
            var turma = new Turma(command.descricao, 0, command.minVagas, unidade.id, sala.id, command.pacoteId, typePacote.typePacoteId, previsao);
            turma.CreateIdentificador(totalTurmas, siglaUnidade);
            turma.SetStatusAndamentoInitial();

            // horario da turma
            var horarios = _mapper.Map<IEnumerable<Horario>>(command.diasSemana);
            turma.AddHorarios(horarios);


            // materias da turma
            turma.AddMaterias(turmaMaterias);



           // await _turmaRepo.Save(turma);
            // _turmaRepo.Commit();


            //Previsoes da turma
            var previ = new Previsoes(command.prevInicio_1, command.prevInicio_2, command.prevInicio_3,
                command.prevTermino_1, command.prevTermino_2, command.prevTermino_3, turma.Id);
           // await _turmaRepo.SavePrevisoes(previ);


            // pegar feriados
            var feriadosValues = await _paramQueries.GetParamValue("Feriados");
            var feriados = new List<DateTime>();
            foreach (var item in feriadosValues)
            {
                var data = item.value.Split("/");
                var dia = Convert.ToInt32(data[0]);
                var mes = Convert.ToInt32(data[1]);

                feriados.Add(new DateTime(1900, mes, dia));

            }




            // CreateCalendarioDaTurma()
            var datas = command.prevInicio_1.EachDay(command.prevTermino_3);
            var datasFiltradas = new List<DateTime>();
            foreach (var dia in command.diasSemana)
            {
                var diaSemana = DiaDaSemana.TryParseToDayofWeek(dia.diaSemana);
                datasFiltradas.AddRange(datas.Where(d => d.DayOfWeek == diaSemana));
            }

            var todasDatas = datasFiltradas.OrderBy(d => d.Date);

            var calendarios = new List<Calendario>();

            var i = 0;
            foreach (var dat in todasDatas)
            {  //0 -1 - 2
                if (i == command.diasSemana.Count()) { i = 0; }

                calendarios.Add(new Calendario(dat, DiaDaSemana.TryParse(dat.DayOfWeek).DisplayName, command.diasSemana[i].horarioInicio, command.diasSemana[i].horarioFim, turmaSalvaId, unidade.id, false, false, command.salaId));

                i++;
            }

            foreach (var item in feriados)
            {
                calendarios.RemoveAll(c => c.DiaAula.Day == item.Day & c.DiaAula.Month == item.Month);
            }


            var pacotesMaterias = await _pacoteQueries.GetMateriasPacote(command.pacoteId);

            var aulasPresenciais = pacotesMaterias.Where(p => p.modalidade == "Presencial").OrderBy(p => p.ordem).ToList();
            // get pacotesMateriaById
            // set materias!
            //var x = calendarios?[300];


            //var qntAulas = 0;

            //try
            //{

            var verificar = aulasPresenciais.OrderBy(a => a.ordem);
            var diaCalendario = 0;
            for (int mat = 0; mat < aulasPresenciais.Count(); mat++)
            {
                double horasTotaisDaMateriaEmMinutos = aulasPresenciais[mat].cargaHoraria * 60;

                while (!DoubleExtensions.NegativeOrZero(horasTotaisDaMateriaEmMinutos))
                {


                    if (diaCalendario >= calendarios.Count()) break;
                    calendarios[diaCalendario].SetMateriaId(aulasPresenciais[mat].materiaId);

                    var horaIni = calendarios[diaCalendario].HoraInicial.Split(":");
                    var horaInicial = new DateTime(2020, 1, 1, Convert.ToInt32(horaIni[0]), Convert.ToInt32(horaIni[1]), 0);

                    var horaFim = calendarios[diaCalendario].HoraFinal.Split(":");
                    var horaFinal = new DateTime(2020, 1, 1, Convert.ToInt32(horaFim[0]), Convert.ToInt32(horaFim[1]), 0);

                    TimeSpan totalMinutos = horaFinal - horaInicial;

                    double minutos = totalMinutos.TotalMinutes;

                    horasTotaisDaMateriaEmMinutos -= minutos;

                    diaCalendario++;
                }


            }

            //}catch(Exception ex)
            //{

            //}



            await _calendarioRepo.SaveCalendarios(calendarios);





            _turmaRepo.Commit();

            return Ok();
        }
    }
}
