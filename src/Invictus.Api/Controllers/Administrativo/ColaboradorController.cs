using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Data.Context;
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
        private readonly InvictusDbContext _db;
        private readonly IColaboradorApplication _colaboradorApplication;
        private readonly IParametrosQueries _paramQueries;
        private readonly IUtils _utils;
        public ColaboradorController(IColaboradorQueries colaboradorQueries, IColaboradorApplication colaboradorApplication, IUtils utils,
            IParametrosQueries paramQueries, InvictusDbContext db)
        {
            _colaboradorQueries = colaboradorQueries;
            _colaboradorApplication = colaboradorApplication;
            _utils = utils;
            _paramQueries = paramQueries;
            _db = db;
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
            //var colaborador = await _colaboradorQueries.GetColaboradoresById(colaboradorId);
            var colaborador = await _colaboradorQueries.GetColaboradoresByIdV2(colaboradorId);

            var values = await _paramQueries.GetParamValue(parametro);

            return Ok(new { colaborador = colaborador, values = values });
        }

        [HttpPost]
        public async Task<IActionResult> SaveColaborador([FromBody] PessoaDto newColaborador)
        {

            //var msg = await _utils.ValidaDocumentosColaborador(newColaborador.cpf, null, newColaborador.email);
            var msg = await _utils.ValidaDocumentoPessoa(newColaborador.cpf, null, newColaborador.email);
            if (msg.Count() > 0) return Conflict(new { msg = msg });

            await _colaboradorApplication.SaveColaboradorV2(newColaborador);

            return NoContent();

           
        }

        [HttpPut]
        public async Task<IActionResult> EditColaborador([FromBody] PessoaDto colaborador)
        {
            await _colaboradorApplication.EditColaboradorV2(colaborador);

            return NoContent();
        }

        [HttpDelete]
        [Route("{colaboradorId}")]
        public async Task<IActionResult> DeleteColaborador(Guid colaboradorId)
        {
            var colab = await _db.Pessoas.FindAsync(colaboradorId);

            _db.Pessoas.Remove(colab);

            _db.SaveChanges();

            return NoContent();
        }

    }
}
