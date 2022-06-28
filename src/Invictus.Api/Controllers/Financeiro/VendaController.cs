using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Dtos.Financeiro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/venda")]
    [Authorize]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly InvictusDbContext _db;
        private readonly IAspNetUser _aspNetUser;
        private List<string> _erros;
        public VendaController(InvictusDbContext db, IAspNetUser aspNetUser)
        {
            _db = db;
            _aspNetUser = aspNetUser;
            _erros = new List<string>();

        }

        [HttpPost]
        //[Route("contas/pagar")]
        public async Task<IActionResult> VendaProduto([FromBody] VendaProdutoCommand command)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            // validar quantidade de itens no etoque, se da unidade e se ativo e os da venda
            // se nao tiver retorna erro 500
            // se tiver, ja diminui a quantidade no banco
            foreach (var produto in command.produtos)
            {
                var result = await _db.Produtos.FindAsync(produto.id);

                if(result == null)
                {
                    return BadRequest();
                }

                if(result.Ativo == true & result.UnidadeId == unidadeId 
                    & result.Quantidade >= produto.quantidade)
                {
                    //vender
                }
                else
                {
                    return BadRequest();
                }
            }

            // diminuir os produtos na base
            foreach (var produto in command.produtos)
            {
                var item = await _db.Produtos.FindAsync(produto.id);

                item.RemoveProduto(produto.quantidade);

                _db.Produtos.Update(item);
            }

            var boletos = new List<Boleto>();
            if (command.parcelar)
            {
                for (int i = 0; i < command.parcelas; i++)
                {

                }
               
            }
            else
            {
                //boletos.Add(Boleto.)
            }
            

            // vender:
                // gerar objetos boleto conforme a qnt parcelas
                // ver parcelas e dar um "foreach"
                // cada boleto gerar log salvando objeto command


            


            return Ok();
        }
    }

    // VendaProdutoCommand
}
