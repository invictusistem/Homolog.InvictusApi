using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/trimestre")]
    [ApiController]
    [Authorize]
    public class TrimestreController : ControllerBase
    {
        private readonly IAgendaTriQueries _agendaTriQueries;
        private readonly IAgendaTriApplication _agendaTriApplication;
        public TrimestreController(IAgendaTriQueries agendaTriQueries, IAgendaTriApplication agendaTriApplication)
        {
            _agendaTriQueries = agendaTriQueries;
            _agendaTriApplication = agendaTriApplication;

        }

        [HttpGet]
        [Route("{unidadeId}/{ano}")]
        public async Task<IActionResult> GetAgendasTrimestres(Guid unidadeId, int ano)
        {
            var agendas = await _agendaTriQueries.GetAgendas(unidadeId, ano);

            return Ok(new { agendas = agendas });
        }

        [HttpGet]
        [Route("{agendaId}")]
        public async Task<IActionResult> GetAgendasTrimestres(Guid agendaId)
        {
            var agenda = await _agendaTriQueries.GetAgenda(agendaId);

            return Ok(new { agenda = agenda });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAgenda([FromBody] AgendaTrimestreDto agenda)
        {
            // TODO conflit com outra agenda salva, só pode salvar pra frente e nao pra trás!!!!!!
            await _agendaTriApplication.AddAgenda(agenda);
            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> EditAgenda([FromBody] AgendaTrimestreDto agenda)
        {
            // TODO conflit com outra agenda salva, só pode editar data Final!
            await _agendaTriApplication.EditAgenda(agenda);

            return NoContent();
        }
    }
}
