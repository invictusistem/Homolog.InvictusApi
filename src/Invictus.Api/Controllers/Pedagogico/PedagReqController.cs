using Invictus.Application.PedagApplication.Interfaces;
using Invictus.Dtos.PedagDto;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Pedagogico
{
    [Route("api/pedag/requerimento")]
    [Authorize]
    [ApiController]
    public class PedagReqController : ControllerBase
    {
        private readonly IPedagReqQueries _reqQueries;
        private readonly IPedagReqService _reqService;

        public PedagReqController(IPedagReqQueries reqQueries, IPedagReqService reqService)
        {
            _reqQueries = reqQueries;
            _reqService = reqService;

        }

        [HttpGet]
        [Route("categorias")]
        public async Task<IActionResult> GetCategorias()
        {

            var categorias = await _reqQueries.GetAllCategorias();

            //if (!categorias.Any()) return NotFound();

            return Ok(new { categorias = categorias });
        }

        [HttpGet]
        [Route("categorias/{id}")]
        public async Task<IActionResult> GetCategoriaById(Guid id)
        {

            var categoria = await _reqQueries.GetCategoriaById(id);

            if (categoria == null) return NotFound();

            return Ok(new { categoria = categoria });
        }

        [HttpGet]
        [Route("tipos/{categoriaId}")]
        public async Task<IActionResult> GetTiposByCategoriaId(Guid categoriaId)
        {

            var tipos = await _reqQueries.GetTiposByCategoriaId(categoriaId);

            //if (!tipos.Any()) return NotFound();

            return Ok(new { tipos = tipos });
        }

        [HttpGet]
        [Route("tipos/busca/{tipoId}")]
        public async Task<IActionResult> GetTipoById(Guid tipoId)
        {

            var tipo = await _reqQueries.GetTipoById(tipoId);

            if (tipo == null) return NotFound();

            return Ok(new { tipo = tipo });
        }

        

        [HttpPut]
        [Route("categorias")]
        public async Task<IActionResult> EditarCategoria([FromBody] CategoriaDto categoria)
        {
            await _reqService.EditCategoria(categoria);

            return Ok();
        }

        [HttpPut]
        [Route("tipos")]
        public async Task<IActionResult> EditarTipo([FromBody] TipoDto tipo)
        {
            await _reqService.EditTipo(tipo);

            return Ok();
        }


        [HttpPost]
        [Route("categorias")]
        public async Task<IActionResult> SalvarCategoria([FromBody] CategoriaDto categoria)
        {
            await _reqService.SaveCategoria(categoria);

            return Ok();
        }

        [HttpPost]
        [Route("tipos")]
        public async Task<IActionResult> SalvarTipo([FromBody] TipoDto tipo)
        {
            await _reqService.SaveTipo(tipo);

            return Ok();
        }



    }
}
