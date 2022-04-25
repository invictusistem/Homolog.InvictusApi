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

        [HttpGet]
        [Route("tipos")]
        public async Task<IActionResult> GetTypeEstagios()
        {

            var tiposEstagios = await _estagioQueries.GetTiposDeEstagios();

            if (!tiposEstagios.Any()) return NotFound();

            return Ok(new { tipos = tiposEstagios });
        }

        [HttpPost]
        public async Task<IActionResult> SaveEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.CreateEstagio(estagio);

            return Ok();
        }

        [HttpPost]
        [Route("tipos")]
        public async Task<IActionResult> EstagioTypeCreate([FromBody] TypeEstagioDto typeEstagio)
        {
            await _estagioApp.CreateTypeEstagio(typeEstagio);

            return Ok();
        }

        [HttpPut]
        [Route("tipos")]
        public async Task<IActionResult> EstagioTypeEdit([FromBody] TypeEstagioDto typeEstagio)
        {
            await _estagioApp.EditTypeEstagio(typeEstagio);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEstagio([FromBody] EstagioDto estagio)
        {
            await _estagioApp.EditEstagio(estagio);

            return Ok();
        }

        [HttpDelete]
        [Route("tipo/{estagioTipoId}")]
        public async Task<IActionResult> EstagioTypeDelete(Guid estagioTipoId)
        {
            await _estagioApp.DeleteTypeEstagio(estagioTipoId);

            return Ok();
        }
    }
}
