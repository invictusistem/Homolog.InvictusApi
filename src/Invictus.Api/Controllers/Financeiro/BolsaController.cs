using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/bolsa")]
    [Authorize]
    [ApiController]
    public class BolsaController : ControllerBase
    {
        private readonly IBolsasQueries _bolsasQueries;
        private readonly IBolsasApp _bolsaApp;
        public BolsaController(IBolsasQueries bolsasQueries, IBolsasApp bolsaApp)
        {
            _bolsasQueries = bolsasQueries;
            _bolsaApp = bolsaApp;
        }

        [HttpGet]
        [Route("{typePacoteId}")]
        public async Task<IActionResult> GetBolsas(Guid typePacoteId)
        {
            var bolsas = await _bolsasQueries.GetBolsas(typePacoteId);

            if (!bolsas.Any()) return NotFound();

            return Ok(new { bolsas = bolsas });
        }

        [HttpGet]
        [Route("senha/{bolsaId}")]
        public async Task<IActionResult> GetSenha(Guid bolsaId)
        {
            var senha = await _bolsasQueries.GetSenha(bolsaId);


            return Ok(new { senha = senha });
        }

        [HttpGet]
        [Route("senha-validar/{senha}")]
        public async Task<IActionResult> GetBolsa(string senha)
        {
            var bolsa = await _bolsasQueries.GetBolsa(senha);

            if (bolsa == null) return NotFound();

            return Ok(new { bolsa = bolsa });
        }

        [HttpPost]
        public async Task<IActionResult> SaveBolsa([FromBody] BolsaDto bolsa)
        {
            var senha = await _bolsaApp.SaveBolsa(bolsa);

            return Ok(new { senha = senha });
        }

    }
}