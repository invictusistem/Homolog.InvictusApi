using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Administrativo
{
    [Route("api/requerimento")]
    [Authorize]
    [ApiController]
    public class RequerimentoController : ControllerBase
    {
        private readonly IRequerimentoQueries _requerimentoQueries;
        private readonly IRequerimentoService _reqService;
        public RequerimentoController(IRequerimentoQueries requerimentoQueries, IRequerimentoService reqService)
        {
            _requerimentoQueries = requerimentoQueries;
            _reqService = reqService;
        }


        // TYPES
        [HttpGet]
        [Route("tipos/{tipoRequerimentoId}")]
        public async Task<IActionResult> TipoRequerimento(Guid tipoRequerimentoId)
        {
            var type = await _requerimentoQueries.GetTipoRequerimentoById(tipoRequerimentoId);

            if (type == null) return NoContent();
           

            return Ok(new { type = type });
        }

        [HttpGet]
        [Route("tipos")]
        public async Task<IActionResult> TipoRequerimentos()
        {
            var types = await _requerimentoQueries.GetTiposRequerimentos();

            if (!types.Any()) return NoContent();

            return Ok(new { types = types });
        }

        [HttpPost]
        [Route("tipos")]
        public async Task<IActionResult> TipoRequerimento(TipoRequerimentoDto requerimento)
        {
            await _reqService.SaveTipoRequerimento(requerimento);

            return Ok();
        }

        [HttpPut]
        [Route("tipos")]
        public async Task<IActionResult> EditTipoRequerimento(TipoRequerimentoDto requerimento)
        {
            await _reqService.EditTipoRequerimento(requerimento);

            return Ok();
        }

        // Requerimentos

        [HttpGet]
        [Route("matricula/{matriculaId}")]
        public async Task<IActionResult> RequerimentosPorMatricula(Guid matriculaId)
        {
            var requerimentos = await _requerimentoQueries.GetRequerimentosByMatriculaId(matriculaId);

            if (requerimentos.Any()) return NoContent();

            return Ok(new { requerimentos = requerimentos });
        }

        [HttpGet]
        public async Task<IActionResult> Requerimentos()
        {
            var requerimentos = await _requerimentoQueries.GetRequerimentosByUnidadeId();

            if (requerimentos.Any()) return NoContent();

            return Ok(new { requerimentos = requerimentos });
        }

        [HttpPost]
        public async Task<IActionResult> Requerimento(RequerimentoDto requerimento)
        {
            await _reqService.SaveRequerimento(requerimento);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditRequerimento(RequerimentoDto requerimento)
        {
            await _reqService.EditRequerimento(requerimento);

            return Ok();
        }

        /*
        

        

        [HttpGet]
        [Route("tipos")]
        public async Task<IActionResult> TipoRequerimentos()
        {
            var types = await _requerimentoQueries.GetTiposRequerimentos();

            if (!types.Any()) return NoContent();

            return Ok(new { types = types });
        }

        [HttpGet]
        [Route("{requerimentoId}")]
        public async Task<IActionResult> Requerimento(Guid requerimentoId)
        {
            var requerimento = await _requerimentoQueries.GetRequerimentoById(requerimentoId);

            if (requerimento == null) return NoContent();

            return Ok(new { requerimento = requerimento });
        }

        [HttpGet]
        [Route("tipos/{tipoRequerimentoId}")]
        public async Task<IActionResult> TipoRequerimento(Guid tipoRequerimentoId)
        {
            var requerimento = await _requerimentoQueries.GetTipoRequerimentoById(tipoRequerimentoId);

            if (requerimento == null) return NoContent();

            var types = await _requerimentoQueries.GetTiposRequerimentos();

            if (!types.Any()) return NoContent();

            return Ok(new { requerimento = requerimento, types = types });
        }

        

        

        [HttpPut]
        public async Task<IActionResult> EditRequerimento(RequerimentoDto requerimento)
        {
            await _reqService.SaveRequerimento(requerimento);

            return Ok();
        }

        
        */

    }
}
