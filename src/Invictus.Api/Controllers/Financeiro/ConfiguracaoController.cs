using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Dtos.Financeiro.Configuracoes;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/configuracao-financ")]
    [Authorize]
    [ApiController]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly IFinancConfigApp _financApp;
        private readonly IFinConfigQueries _finConfigQuereis;
        public ConfiguracaoController(IFinancConfigApp financApp, IFinConfigQueries finConfigQuereis)
        {
            _financApp = financApp;
            _finConfigQuereis = finConfigQuereis;
        }


        [HttpGet]
        [Route("banco")]
        public async Task<IActionResult> GetBanco()
        {
            var result = await _finConfigQuereis.GetAllBancos();
            if (!result.Any()) return NotFound();
            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("centro-custo")]
        public async Task<IActionResult> GetCentroCusto()
        {
            var result = await _finConfigQuereis.GetAllCentroCusto();
            if (!result.Any()) return NotFound();
            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("meio-pgm")]
        public async Task<IActionResult> GetMeioPgm()
        {
            var result = await _finConfigQuereis.GetAllMeiosPagamento();
            if (!result.Any()) return NotFound();
            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("plano")]
        public async Task<IActionResult> GetPlano()
        {
            var result = await _finConfigQuereis.GetAllPlanos();
            if (!result.Any()) return NotFound();
            return Ok(new { result = result });
        }

        [HttpGet]
        [Route("subconta")]
        public async Task<IActionResult> GetSubConta()
        {
            var result = await _finConfigQuereis.GetAllSubContas();
            if (!result.Any()) return NotFound();
            return Ok(new { result = result });
        }

        // POST

        [HttpPost]
        [Route("banco")]
        public async Task<IActionResult> SaveBanco([FromBody] BancoDto bancoDto)
        {
            await _financApp.SaveBanco(bancoDto);
            return Ok();
        }

        [HttpPost]
        [Route("centro-custo")]
        public async Task<IActionResult> SaveCentroCusto([FromBody] CentroCustoDto centroCustoDto)
        {
            await _financApp.SaveCentroDeCusto(centroCustoDto);
            return Ok();
        }

        [HttpPost]
        [Route("meio-pgm")]
        public async Task<IActionResult> SaveMeioPgm([FromBody] MeioPagamentoDto meioPagamentoDto)
        {
            await _financApp.SaveMeioDePagamento(meioPagamentoDto);
            return Ok();
        }

        [HttpPost]
        [Route("plano")]
        public async Task<IActionResult> SavePlano([FromBody] PlanoContaDto planoContaDto)
        {
            await _financApp.SavePlanoDeConta(planoContaDto);
            return Ok();
        }

        [HttpPost]
        [Route("subconta")]
        public async Task<IActionResult> SaveSubConta([FromBody] SubContaDto subContaDto)
        {
            await _financApp.SaveSubConta(subContaDto);
            return Ok();
        }
    }
}
