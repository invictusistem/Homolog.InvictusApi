using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [Route("api/contrato")]
    [Authorize]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoQueries _contratoQuery;
        private readonly IContratoApplication _contratoApplication;
        public ContratoController(IContratoQueries contratoQuery,
                                 IContratoApplication contratoApplication
            )
        {
            _contratoQuery = contratoQuery;
            _contratoApplication = contratoApplication;
        }

        [HttpGet]
        //[Route("contrato")]
        public async Task<IActionResult> GetContratos()
        {
            var contratos = await _contratoQuery.GetContratosViewModel();//.CreateUnidade(command);

            return Ok(new { contratos = contratos });
        }

        [HttpGet]
        [Route("{contratoId}")]
        public async Task<ActionResult> GetContratoById(Guid contratoId)
        {

            var contrato = await _contratoQuery.GetContratoById(contratoId);

            return Ok(new { contrato = contrato });

        }

        [HttpGet]
        [Route("type-pacote/{typePacoteId}")]
        public async Task<ActionResult> GetContratoByTypePacote(Guid typePacoteId)
        {

            var contratos = await _contratoQuery.GetContratoByTypePacote(typePacoteId);

            return Ok(new { contratos = contratos });

        }

        [HttpPost]
        //[Route("contrato")]
        public async Task<IActionResult> SaveContrato([FromBody] ContratoDto newContrato)
        {
            await _contratoApplication.AddContrato(newContrato);

            return Ok();
        }

        [HttpPut]
       // [Route("contrato")]
        public async Task<IActionResult> EditContrato([FromBody] ContratoDto editedContrato)
        {
            await _contratoApplication.EditContrato(editedContrato);

            return Ok();
        }
    }
}
