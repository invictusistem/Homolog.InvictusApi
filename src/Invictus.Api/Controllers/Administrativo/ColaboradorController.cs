using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/colaboradores")]
    [Authorize]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorQueries _colaboradorQueries;
        private readonly IColaboradorApplication _colaboradorApplication;
        private readonly IParametrosQueries _paramQueries;
        private readonly IUtils _utils;
        public ColaboradorController(IColaboradorQueries colaboradorQueries, IColaboradorApplication colaboradorApplication, IUtils utils,
            IParametrosQueries paramQueries)
        {
            _colaboradorQueries = colaboradorQueries;
            _colaboradorApplication = colaboradorApplication;
            _utils = utils;
            _paramQueries = paramQueries;
        }
       

        [HttpGet]
        [Route("pesquisar")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetColaboradorV2([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var results = await _colaboradorQueries.GetColaboradoresByUnidadeId(itemsPerPage, currentPage, paramsJson);
            
            if (results.Data.Count() == 0) return NotFound();

            return Ok(results);
        }

        [HttpGet]
        [Route("{parametro}/{colaboradorId}")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetColaborador(string parametro, Guid colaboradorId)
        {
            var colaborador = await _colaboradorQueries.GetColaboradoresById(colaboradorId);

            var values = await _paramQueries.GetParamValue(parametro);

            return Ok(new { colaborador = colaborador, values = values });
        }

        [HttpPost]
        public async Task<IActionResult> SaveColaborador([FromBody] ColaboradorDto newColaborador)
        {

            //var msg = await _utils.ValidaDocumentosColaborador(newColaborador.cpf, null, newColaborador.email);
            //if (msg.Count() > 0) return Conflict(new { msg = msg });

            //await _colaboradorApplication.SaveColaborador(newColaborador);

            //return NoContent();

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> EditColaborador([FromBody] ColaboradorDto colaborador)
        {
            await _colaboradorApplication.EditColaborador(colaborador);

            return NoContent();
        }

    }
}
