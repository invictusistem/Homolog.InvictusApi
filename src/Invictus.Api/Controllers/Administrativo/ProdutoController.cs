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
    [Route("api/produto")]
    [Authorize]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoQueries _produtoQueries;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IProdutoApplication _produtoApplication;
        public ProdutoController(IProdutoQueries produtoQueries, IProdutoApplication produtoApplication, IUnidadeQueries unidadeQueries)
        {
            _produtoQueries = produtoQueries;
            _unidadeQueries = unidadeQueries;
            _produtoApplication = produtoApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Produtos()
        {
            var produtos = await _produtoQueries.GetProdutos();

            return Ok(new { produtos = produtos });
        }

        [HttpGet]
        [Route("doacao/{produtoId}/{siglaUnidade}")]
        public async Task<IActionResult> ProdutoDoacao(Guid produtoId, string siglaUnidade)
        {
            var unidades = await _unidadeQueries.GetUnidadesDonatarias(siglaUnidade);
            if (unidades.Count() == 0) return NotFound();

            var produto = await _produtoQueries.GetProdutobyId(produtoId);

            return Ok(new { produto = produto, unidades = unidades });
        }

        [HttpGet]
        [Route("{produtoId}")]
        public async Task<IActionResult> Produto(Guid produtoId)
        {
            var produto = await _produtoQueries.GetProdutobyId(produtoId);// .GetProdutosViewModel();//  _db.Produtos.ToListAsync();

            return Ok(new { produto = produto });
        }

        [HttpGet]
        [Route("busca")]
        public async Task<IActionResult> BuscaProduto([FromQuery] string nome)
        {   
            var produtos = await _produtoQueries.BuscaProduto(nome);// .GetProdutosViewModel();//  _db.Produtos.ToListAsync();

            if (!produtos.Any()) return NotFound();

            return Ok(new { produtos = produtos });
        }

        [HttpPost]
        // [Route("produto")]
        public async Task<IActionResult> Save(ProdutoDto newProduto)
        {
            var nomes = await _produtoQueries.SearchProductByName(newProduto.nome);

            if (nomes.Any()) return Conflict();

            await _produtoApplication.AddProduto(newProduto);

            return Ok();
        }

        [HttpPost]
        [Route("doacao")]
        public async Task<IActionResult> DoacaoUnidade(DoacaoCommand doacaoCommand)
        {

            await _produtoApplication.DoarEntreUnidades(doacaoCommand);

            return Ok();
        }

        [HttpPut]
        // [Route("produto")]
        public async Task<IActionResult> Edit(ProdutoDto editedProduto)
        {
            await _produtoApplication.EditProduto(editedProduto);
            // TODO LOG QUEM SALVOU
            return NoContent();
        }
    }
}
