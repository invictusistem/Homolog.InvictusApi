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
    [Route("api/plano-pagamento")]
    [ApiController]
    [Authorize]
    public class PlanoPagamentoController : ControllerBase
    {
        private readonly IPlanoPagamentoQueries _planoQueries;
        private readonly IPlanoPagamentoApplication _planoApplication;
        private readonly IContratoQueries _contratoQueries;
        private readonly ITypePacoteQueries _typePacoteQueries;
        //private readonly IAdmApplication _admApplication;
        public PlanoPagamentoController(IPlanoPagamentoQueries planoQueries, IContratoQueries contratoQueries,
            ITypePacoteQueries typePacoteQueries, IPlanoPagamentoApplication planoApplication)
        {
            _planoQueries = planoQueries;
            _contratoQueries = contratoQueries;
            _typePacoteQueries = typePacoteQueries;
            _planoApplication = planoApplication;
            //_admApplication = admApplication;
        }

        [HttpGet]
        [Route("create-plano")]
        public async Task<IActionResult> GetCreatePlanoViewModel()
        {
            var typePacotes = await _typePacoteQueries.GetTypePacotes();

            var contratos = await _contratoQueries.GetContratosViewModel();//.GetContratos();

            return Ok(new { typePacotes = typePacotes, contratos = contratos });
        }

        [HttpGet]
        [Route("pacote/{typePacoteId}")]
        public async Task<IActionResult> GetPlanosByTypePacote(Guid typePacoteId)
        {
            var planos = await _planoQueries.GetPlanosByTypePacote(typePacoteId);

            return Ok(new { planos = planos });
        }

        [HttpGet]
        [Route("{planoId}")]
        public async Task<IActionResult> GetPlanoByIde(Guid planoId)
        {
            var plano = await _planoQueries.GetPlanoById(planoId);

            var contratos = await _contratoQueries.GetContratoByTypePacote(plano.typePacoteId, false);

            var typePacotes = await _typePacoteQueries.GetTypePacotes();

            return Ok(new { plano = plano, typePacotes = typePacotes, contratos = contratos });
        }

        [HttpPost]
        public async Task<IActionResult> SavePlano([FromBody] PlanoPagamentoDto plano)
        {
            await _planoApplication.SavePlano(plano);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditPlano([FromBody] PlanoPagamentoDto plano)
        {
            await _planoApplication.EditPlano(plano);

            return Ok();
        }

    }
}
