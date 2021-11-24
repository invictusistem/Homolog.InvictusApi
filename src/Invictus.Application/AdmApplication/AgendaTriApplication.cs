using AutoMapper;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Core.Interfaces;
using Invictus.Domain.Administrativo.AgendaTri.Interfaces;
using Invictus.Domain.Administrativo.Models;

using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using System.Threading.Tasks;

namespace Invictus.Application.AdmApplication
{
    public class AgendaTriApplication : IAgendaTriApplication
    {
        
        private readonly IMapper _mapper;
       // private readonly IAspNetUser _aspNetUser;
        private readonly IAgendaTriRepository _agendaRepo;
        public AgendaTriApplication(IMapper mapper, //IAspNetUser aspNetUser,
            IAgendaTriRepository agendaRepo
            )
        {   
            _mapper = mapper;
           // _aspNetUser = aspNetUser;
            _agendaRepo = agendaRepo;
        }
        public async Task AddAgenda(AgendaTrimestreDto agendaDto)
        {
            var agenda = _mapper.Map<AgendaTrimestre>(agendaDto);
            await _agendaRepo.AddAgendaTrimestre(agenda);
            _agendaRepo.Commit();
        }

        public async Task EditAgenda(AgendaTrimestreDto agendaDto)
        {
            var agenda = _mapper.Map<AgendaTrimestre>(agendaDto);
            await _agendaRepo.EditAgendaTrimestre(agenda);
            _agendaRepo.Commit();
        }
    }
}
