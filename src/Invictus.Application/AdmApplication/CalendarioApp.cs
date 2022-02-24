using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Domain.Administrativo.Calendarios;
using Invictus.Domain.Administrativo.Calendarios.Interfaces;
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
    public class CalendarioApp : ICalendarioApp
    {
        private readonly ICalendarioQueries _calendarioQueries;
        private readonly ICalendarioRepo _calendarioRepo;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IMapper _mapper;
        public CalendarioApp(ICalendarioQueries calendarioQueries, IMapper mapper, ICalendarioRepo calendarioRepo, ITurmaQueries turmaQueries)
        {
            _calendarioQueries = calendarioQueries;
            _mapper = mapper;
            _calendarioRepo = calendarioRepo;
            _turmaQueries = turmaQueries;
        }
        public async Task<TurmaCalendarioViewModel> EditCalendario(AulaViewModel aula, Guid calendarioId)
        {
            // trazer calendario atual
            var calen = await _calendarioQueries.GetCalendarioById(calendarioId);
            calen.diaAula = new DateTime(aula.diaAula.Year, aula.diaAula.Month, aula.diaAula.Day, 0, 0, 0);
            
            calen.horaInicial = aula.horaInicial;
            calen.horaFinal = aula.horaFinal;

            var turmasMaterias = await _turmaQueries.GetTurmaMateriaByTurmaAndMateriaId(aula.materiaId, calen.turmaId);
            var calendario = _mapper.Map<Calendario>(calen);
            

            


            // ver se sala é diferente, atualizar
            if(aula.salaId != calendario.SalaId)
            {
                calendario.SetSalaId(aula.salaId);
            }

            // se materia é diferente, atualizar
            if (aula.materiaId != calendario.MateriaId)
            {
                calendario.SetMateriaId(aula.materiaId);
            }

            // se prof é diferente 
            if (aula.professorId != calendario.ProfessorId)
            {
                calendario.SetProfessorId(aula.professorId);
                calendario.VerificarSeSubstituto(turmasMaterias.professorId, aula.professorId);
            }

            await _calendarioRepo.UpdateCalendario(calendario);

            _calendarioRepo.Commit();

            var editedAula = await _calendarioQueries.GetCalendarioViewModelById(calendarioId);

            return editedAula;


        }
    }
}
