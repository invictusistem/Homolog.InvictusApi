using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/template")]
    [Authorize]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateQueries _templateQueries;
        //private readonly ITemplateApplication _templateApplication;
        public TemplateController(ITemplateQueries templateQueries//, ITemplateApplication templateApplication
            )
        {
            _templateQueries = templateQueries;
            //_templateApplication = templateApplication;
        }

        
        // Planos Pagamentos

        [HttpGet]
        [Route("plano-pagamento/{itemsPerPage}/{currentPage}")]
        public async Task<IActionResult> GetPlanos(int itemsPerPage, int currentPage)
        {

            var results = await _templateQueries.GetListPlanoPagamentoTemplate(itemsPerPage, currentPage);

            // var cargos = await _db.Cargos.ToListAsync();
            if (results.Data.Count() == 0) return NotFound();

            return Ok(new { results = results });
        }

        [HttpGet]
        [Route("plano-pagamento/{planoId}")]
        public async Task<IActionResult> GetPlanosById(Guid planoId)
        {

            var result = await _templateQueries.GetPagamentoTemplateById(planoId);

            // var cargos = await _db.Cargos.ToListAsync();
            if (result == null) return NotFound();

            return Ok(new { result = result });
        }

        [HttpPost]
        [Route("plano-pagamento")]
        public async Task<IActionResult> SavePlano([FromBody] PlanoPagamentoDto plano)
        {
           // await _templateApplication.AddPlano(plano);

            return NoContent();
        }

        [HttpPut]
        [Route("plano-pagamento")]
        public async Task<IActionResult> EditPlano([FromBody] PlanoPagamentoDto plano)
        {
           // await _templateApplication.EditPlano(plano);

            return NoContent();
        }
    }
}
