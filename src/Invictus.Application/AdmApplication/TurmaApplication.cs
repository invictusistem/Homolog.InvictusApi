using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.Extensions;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.TurmaAggregate;
using Invictus.Domain.Administrativo.TurmaAggregate.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
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
        private readonly IAspNetUser _aspNetUser;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IPacoteQueries _pacoteQueries;
        private readonly IMapper _mapper;
        private readonly ITurmaRepo _turmaRepo;
        public TurmaApplication(IUnidadeQueries unidadeQueries, IAspNetUser aspNetUser, ITurmaQueries turmaQueries,
            IPacoteQueries pacoteQueries,IMapper mapper,ITurmaRepo turmaRepo)
        {
            _unidadeQueries = unidadeQueries;
            _aspNetUser = aspNetUser;
            _turmaQueries = turmaQueries;
            _pacoteQueries = pacoteQueries;
            _mapper = mapper;
            _turmaRepo = turmaRepo;
        }
        public async Task CreateTurma(CreateTurmaCommand command)
        {
            //var diasQnt = command.diasSemana.Count();
            var sala = await _unidadeQueries.GetSala(command.salaId);
            var siglaUnidade = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(siglaUnidade);
            var totalTurmas = await _turmaQueries.CountTurmas(unidade.id);
            var typePacote = await _pacoteQueries.GetPacoteById(command.pacoteId);
            var previsao = new Previsao(command.prevInicio_1, command.prevTermino_1, "1ª previsão", DateTime.Now);
            var turma = new Turma(command.descricao, 0, command.minVagas, unidade.id, sala.id, command.pacoteId, typePacote.typePacoteId, previsao);
            turma.CreateIdentificador(totalTurmas, siglaUnidade);
            turma.SetStatusAndamentoInitial();
            
            var horarios = _mapper.Map<IEnumerable<Horario>>(command.diasSemana);

            turma.AddHorarios(horarios);

            var materias = await _turmaQueries.GetMateriasFromPacotesMaterias(command.pacoteId);

            var turmaMaterias = _mapper.Map<IEnumerable<TurmaMaterias>>(materias);

            turma.AddMaterias(turmaMaterias);

            await _turmaRepo.Save(turma);
            _turmaRepo.Commit();

            var previ = new Previsoes(command.prevInicio_1, command.prevInicio_2, command.prevInicio_3,
                command.prevTermino_1, command.prevTermino_2, command.prevTermino_3, turma.Id);

            await _turmaRepo.SavePrevisoes(previ);

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
                if(i == command.diasSemana.Count()) { i = 0; }

                calendarios.Add(new Calendario(dat, DiaDaSemana.TryParse(dat.DayOfWeek).DisplayName,command.diasSemana[i].horarioInicio, command.diasSemana[i].horarioFim, turma.Id, unidade.id, false, false, command.salaId));

                i++;
            }

            var pacotesMaterias = await _pacoteQueries.GetMateriasPacote(command.pacoteId);
            // get pacotesMateriaById



           var diasQnt = command.diasSemana.Count();





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
            foreach (var item in profsMatCommand)
            {
                var turmaMatDto = await _turmaQueries.GetTurmaMateria(item.id);
                var turmaMat = _mapper.Map<TurmaMaterias>(turmaMatDto);

                if (item.isProfessor)
                {
                    turmaMat.AddProfessorNaMateria(professorId);
                }
                else
                {
                    turmaMat.RemoveProfessorDaMateria();
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

            if(turmasMaterias.Count() > 0)
            {
                foreach (var item in turmasMaterias)
                {
                    item.RemoveProfessorDaMateria();
                }

                _turmaRepo.AtualizarTurmasMaterias(turmasMaterias);
            }

            _turmaRepo.Commit();
        }
    }
}
