using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.Utilitarios.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IUtils _utils;
        public ColaboradorController(IColaboradorQueries colaboradorQueries, IColaboradorApplication colaboradorApplication, IUtils utils)
        {
            _colaboradorQueries = colaboradorQueries;
            _colaboradorApplication = colaboradorApplication;
            _utils = utils;
        }
       

        [HttpGet]
        [Route("pesquisar")]
        public async Task<ActionResult<PaginatedItemsViewModel<ColaboradorDto>>> GetColaboradorV2([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var results = await _colaboradorQueries.GetColaboradoresByUnidadeId(itemsPerPage, currentPage, paramsJson);
            
            if (results.Data.Count() == 0) return NotFound();

            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> SaveColaborador([FromBody] ColaboradorDto newColaborador)
        {  

            var msg = await _utils.ValidaDocumentosColaborador(newColaborador.cpf, null, newColaborador.email);
            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _colaboradorApplication.SaveColaborador(newColaborador);
            
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> EditColaborador([FromBody] ColaboradorDto colaborador)
        {
            await _colaboradorApplication.EditColaborador(colaborador);

            return NoContent();
        }

    }
}
