using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.AdmDtos.Utils;
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
    [Route("api/fornecedores")]
    [Authorize]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorQueries _fornecedorqueries;
        private readonly IFornecedorApp _fornecedorApp;
        public FornecedoresController(IFornecedorQueries fornecedorqueries, IFornecedorApp fornecedorApp)
        {
            _fornecedorqueries = fornecedorqueries;
            _fornecedorApp = fornecedorApp;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItemsViewModel<PessoaDto>>> GetFornecedores([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var fornecedores = await _fornecedorqueries.GetFornecedores(itemsPerPage, currentPage, paramsJson);

            if (fornecedores.Data.Count() == 0) return NotFound();

            return Ok(fornecedores);
        }

        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAllFornecedores()
        {
            var fornecedores = await _fornecedorqueries.GetAllFornecedores();

            //if (!fornecedores.Count() == 0) return NotFound();

            return Ok(new { fornecedores = fornecedores });
        }

        [HttpGet]
        [Route("colaboradores-e-professores")]
        public async Task<IActionResult> GetAllFornecedoresAndProfessores()
        {
            var colaboradores = await _fornecedorqueries.GetAllColaboradoresAndProfessores();

            //if (!fornecedores.Count() == 0) return NotFound();

            return Ok(new { colaboradores = colaboradores });
        }

        [HttpGet]
        [Route("{fornecedorId}")]
        public async Task<IActionResult> GetFornecedor(Guid fornecedorId)
        {
            var fornecedor = await _fornecedorqueries.GetFornecedor(fornecedorId);

            return Ok(new { fornecedor = fornecedor });
        }

        [HttpPost]
        public async Task<IActionResult> SaveFornecedor([FromBody] PessoaDto fornecedor)
        {
            await _fornecedorApp.CreateFornecedor(fornecedor);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFornecedor([FromBody] PessoaDto fornecedor)
        {
            await _fornecedorApp.UpdateFornecedor(fornecedor);

            return Ok();
        }


    }
}
