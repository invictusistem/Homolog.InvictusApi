using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/estagio")]
    [Authorize]
    [ApiController]
    public class EstagioController : ControllerBase
    {
        private readonly IEstagioApplication _estagioApp;
        private readonly IEstagioQueries _estagioQueries;
        private readonly IAlunoQueries _alunoQueries;
        public EstagioController(IEstagioApplication estagioApp, IEstagioQueries estagioQueries, IAlunoQueries alunoQueries)
        {
            _estagioApp = estagioApp;
            _estagioQueries = estagioQueries;
            _alunoQueries = alunoQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstagios()
        {
            var estagios = await _estagioQueries.GetEstagios();
            
            if (!estagios.Any()) return NotFound();

            return Ok(new { estagios = estagios });
        }

        [HttpGet]
        [Route("{estagioId}")]
        public async Task<IActionResult> GetEstagio(Guid estagioId)
        {
            var estagio = await _estagioQueries.GetEstagioById(estagioId);

            return Ok(new { estagio = estagio });
        }

        [HttpGet]
        [Route("alunos")]
        public async Task<IActionResult> GetAlunoByFilter([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            //var results = await _alunoQueries.GetMatriculadosView(itemsPerPage, currentPage, paramsJson);

            var estagioarios = await _estagioQueries.GetMatriculadosView(itemsPerPage, currentPage, paramsJson);

            if (estagioarios.Data.Count() == 0) return NotFound();

            return Ok(estagioarios);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.CreateEstagio(estagio);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.EditEstagio(estagio);

            return Ok();
        }
    }
}
