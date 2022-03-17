using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.Extensions;
using Invictus.Core;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Domain.Padagogico.NotasTurmas;
using Invictus.Domain.Padagogico.NotasTurmas.Interface;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class TurmaApplication : ITurmaApplication
    {
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly ICalendarioQueries _calendarioQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly ICalendarioRepo _calendarioRepo;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IPacoteQueries _pacoteQueries;
        private readonly IParametrosQueries _paramQueries;
        private readonly IMapper _mapper;
        private readonly ITurmaRepo _turmaRepo;
        private readonly ITurmaNotasRepo _notasRepo;
        private readonly InvictusDbContext _db;
        public TurmaApplication(IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser, ITurmaQueries turmaQueries,
            IPacoteQueries pacoteQueries, IMapper mapper, ITurmaRepo turmaRepo, IParametrosQueries paramQueries,
            ICalendarioRepo calendarioRepo, ITurmaNotasRepo notasRepo, ICalendarioQueries calendarioQueries, InvictusDbContext db)
        {
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
            _turmaQueries = turmaQueries;
            _pacoteQueries = pacoteQueries;
            _mapper = mapper;
            _turmaRepo = turmaRepo;
            _paramQueries = paramQueries;
            _calendarioRepo = calendarioRepo;
            _notasRepo = notasRepo;
            _calendarioQueries = calendarioQueries;
            _db = db;
        }
        public async Task CreateTurma(CreateTurmaCommand command)
        {
            //var diasQnt = command.diasSemana.Count();
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



            await _turmaRepo.Save(turma);
            // _turmaRepo.Commit();


            //Previsoes da turma
            var previ = new Previsoes(command.prevInicio_1, command.prevInicio_2, command.prevInicio_3,
                command.prevTermino_1, command.prevTermino_2, command.prevTermino_3, turma.Id);
            await _turmaRepo.SavePrevisoes(previ);


            // pegar feriados
            // var feriadosValues = await _paramQueries.GetParamValue("Feriados");
            // var feriados = new List<DateTime>();
            //foreach (var item in feriadosValues)
            //{
            //    var data = item.value.Split("/");
            //    var dia = Convert.ToInt32(data[0]);
            //    var mes = Convert.ToInt32(data[1]);

            //    feriados.Add(new DateTime(1900, mes, dia));

            //}




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

                calendarios.Add(new Calendario(dat, DiaDaSemana.TryParse(dat.DayOfWeek).DisplayName, command.diasSemana[i].horarioInicio, command.diasSemana[i].horarioFim, turma.Id, unidade.id, false, false, command.salaId));

                i++;
            }

            //foreach (var item in feriados)
            //{
            //    calendarios.RemoveAll(c => c.DiaAula.Day == item.Day & c.DiaAula.Month == item.Month);
            //}


            var pacotesMaterias = await _pacoteQueries.GetMateriasPacote(command.pacoteId);

            var aulasPresenciais = pacotesMaterias.Where(p => p.modalidade == "Presencial").ToList();
            // get pacotesMateriaById
            // set materias!
            //var x = calendarios?[300];


            //var qntAulas = 0;

            try
            {
                var diaCalendario = 0;
            for (int mat = 0; mat < aulasPresenciais.Count(); mat++)
            {
                double horasTotaisDaMateriaEmMinutos = aulasPresenciais[mat].cargaHoraria * 60;

                while (!DoubleExtensions.NegativeOrZero(horasTotaisDaMateriaEmMinutos))
                {


                    if (diaCalendario >= calendarios.Count()) return;
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

            }
            catch (Exception ex)
            {

            }



            await _calendarioRepo.SaveCalendarios(calendarios);





            _turmaRepo.Commit();

        }

        public async Task IniciarTurma(Guid turmaId)
        {
            await _turmaRepo.IniciarTurma(turmaId);
            // deletar calendario e refazer para a proxima data
            // _turmaRepo.Commit();
        }

        public async Task AdiarInicio(Guid turmaId)
        {
            await _turmaRepo.AdiarInicio(turmaId);
            // depois de adiar refazer calendario, deletar todos registros e refazer
            //_turmaRepo.Commit();
        }

        public async Task AddProfessoresNaTurma(SaveProfsCommand command)
        {

            var professores = new List<TurmaProfessor>();
            foreach (var id in command.listProfsIds)
            {
                professores.Add(new TurmaProfessor(id, command.turmaId));
            }

            await _turmaRepo.AddProfsNaTurma(professores);

            _turmaRepo.Commit();
        }

        public async Task SetMateriaProfessor(Guid turmaId, Guid professorId, IEnumerable<MateriaView> profsMatCommand)
        {

            // TODO CALENDARIO
            // tirar tudo dele
            // colocar onde está true
            foreach (var item in profsMatCommand)
            {
                var turmaMatDto = await _turmaQueries.GetTurmaMateria(item.id);
                var turmaMat = _mapper.Map<TurmaMaterias>(turmaMatDto);

                if (item.isProfessor)
                {
                    turmaMat.AddProfessorNaMateria(professorId);
                    // colocar no calendario
                    var calendarios = _mapper.Map<IEnumerable<Calendario>>(await _calendarioQueries.GetFutureCalendarsByTurmaIdAndMateriaId(turmaMatDto.materiaId, turmaId));
                    //await _db.Calendarios.Where(c => c.TurmaId == turmaId & c.MateriaId == turmaMatDto.materiaId).ToListAsync();

                    if (calendarios.Any())
                    {
                        calendarios.ForEach(c => c.SetProfessorId(professorId));
                        _calendarioRepo.UpdateCalendarios(calendarios.ToList());
                    }
                }
                else
                {
                    turmaMat.RemoveProfessorDaMateria();
                    var calendarios = _mapper.Map<IEnumerable<Calendario>>(await _calendarioQueries.GetFutureCalendarsByTurmaIdAndMateriaId(turmaMatDto.materiaId, turmaId));
                   
                    if (calendarios.Any())
                    {
                        calendarios.ForEach(c => c.RemoveProfessorDaAula());
                        _calendarioRepo.UpdateCalendarios(calendarios.ToList());
                    }                    
                }
                await _turmaRepo.UpdateMateriaDaTurma(turmaMat);
            }
            _turmaRepo.Commit();
        }

        public async Task RemoverProfessorDaTurma(Guid professorId, Guid turmaId)
        {
            var turmaProfDto = await _turmaQueries.GetTurmaProfessor(professorId, turmaId);

            var turmaProf = _mapper.Map<TurmaProfessor>(turmaProfDto);

            await _turmaRepo.RemoverProfessorDaTurma(turmaProf);

            var turmasMateriasDto = await _turmaQueries.GetTurmaMateriasFromProfessorId(professorId, turmaId);

            var turmasMaterias = _mapper.Map<List<TurmaMaterias>>(turmasMateriasDto);

            // puxar calendarios da turma e materia do professor
            var calendarios = await _db.Calendarios.Where(c => c.TurmaId == turmaId & c.ProfessorId == professorId).ToListAsync();
            // filtrar por materias, se TODAS as datas ja passaram, incluindo hoje, nao tirar prof
            // se metade passou e outra metade nao.. nao tirar prof
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            var calendDistinct = calendarios.DistinctBy(c => c.MateriaId);
            foreach (var cal in calendDistinct)
            {
                var filtro = calendarios.Where(c => c.DiaAula <= hoje & c.MateriaId == cal.MateriaId);

                if (!filtro.Any())
                {
                    calendarios.Where(c => c.MateriaId == cal.MateriaId).ForEach(c => c.RemoveProfessorDaAula());
                }
            }

            _calendarioRepo.UpdateCalendarios(calendarios);
            _calendarioRepo.Commit();

            if (turmasMaterias.Count() > 0)
            {
                foreach (var item in turmasMaterias)
                {
                    item.RemoveProfessorDaMateria();
                }

                _turmaRepo.AtualizarTurmasMaterias(turmasMaterias);
            }

            _turmaRepo.Commit();
        }

        public async Task UpdateNotas(List<TurmaNotasDto> notas)
        {
            foreach (var item in notas)
            {
                if (String.IsNullOrEmpty(item.avaliacaoUm))
                    item.avaliacaoUm = null;

                if (String.IsNullOrEmpty(item.avaliacaoDois))
                    item.avaliacaoDois = null;

                if (String.IsNullOrEmpty(item.avaliacaoTres))
                    item.avaliacaoTres = null;

                if (String.IsNullOrEmpty(item.segundaChamadaAvaliacaoUm))
                    item.segundaChamadaAvaliacaoUm = null;

                if (String.IsNullOrEmpty(item.segundaChamadaAvaliacaoDois))
                    item.segundaChamadaAvaliacaoDois = null;

                if (String.IsNullOrEmpty(item.segundaChamadaAvaliacaoTres))
                    item.segundaChamadaAvaliacaoTres = null;

            }

            var listaNotas = new List<TurmaNotas>();

            foreach (var nota in notas)
            {
                var notasDisc = TurmaNotas.CreateNota(nota.id, nota.avaliacaoUm, nota.segundaChamadaAvaliacaoUm, nota.avaliacaoDois, nota.segundaChamadaAvaliacaoDois,
                    nota.avaliacaoTres, nota.segundaChamadaAvaliacaoTres, nota.materiaId, nota.materiaDescricao, nota.matriculaId, nota.turmaId, ResultadoNotas.TryParse(nota.resultado));

                notasDisc.VerificarStatusResultado();

                listaNotas.Add(notasDisc);


            }

            _notasRepo.UpdateNotas(listaNotas);

            _notasRepo.Commit();

        }

        public async Task SavePresenca(AulaDiarioClasseViewModel saveCommand)
        {
            var presencaList = _mapper.Map<List<Presenca>>(saveCommand.listaPresenca);

            foreach (var presenca in presencaList)
            {
                presenca.SetPresenca(presenca.IsPresentToString);                
            }
           
            //var calendarioDto = await _calendarioQueries.GetCalendarioById(saveCommand.aulaViewModel.id);// _context.Calendarios.Find(savePresencaCommand.calendarId);

            //var calendario = _mapper.Map<Calendario>(calendarioDto);

            //calendario.SetObservacoes(saveCommand.aulaViewModel.observacoes);
            //calendario.SetDataConclusaoAula();
            //calendario.ConcluirAula();

            //await _calendarioRepo.UpdateCalendario(calendario);

            _turmaRepo.UpdatePresencas(presencaList);

            _turmaRepo.Commit();
        }
    }
}
