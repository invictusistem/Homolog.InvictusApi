using Dapper;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Logs;
using Invictus.Domain.Financeiro;
using Invictus.Dtos.Financeiro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly IConfiguration _config;
        private List<string> _erros;
        public VendaController(InvictusDbContext db, IAspNetUser aspNetUser, IConfiguration config)
        {
            _db = db;
            _aspNetUser = aspNetUser;
            _erros = new List<string>();
            _config = config;
        }

        [HttpGet]
        [Route("busca")]
        public async Task<IActionResult> BuscaProduto([FromQuery] DateTime start, [FromQuery] DateTime end)
        {

            var inicio = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            var final = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);

            IEnumerable< ProdutoVendasViewModel> results = new List<ProdutoVendasViewModel>();

            var query = @"SELECT 
                        Boletos.id,
                        boletos.Historico as descricao,
                        Boletos.ValorPago as valorTotal,
                        Boletos.DataCompensacao ,
                        boletos.DataPagamento as DataVenda,
                        FormasRecebimento.Descricao as meioPagamento
                        FROM Boletos
                        LEFT JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
                        WHERE Boletos.DataPagamento > @inicio
                        AND Boletos.DataPagamento < @final ";

            await using (var connection = new SqlConnection(
                    _config.GetConnectionString("InvictusConnection")))
            {
                connection.Open();

                results = await connection.QueryAsync<ProdutoVendasViewModel>(query, new { inicio = inicio, final = final });

                connection.Close();
            }


            //info dos logs
            //foreach (var item in results)
            //{
            //    var htmlMessage = "";
            //    var infos = await _db.LogBoletos.Where(l => l.BoletoId == item.id).ToListAsync();//.SingleOrDefaultAsync();

            //    var logCommand = JsonConvert.DeserializeObject<VendaProdutoCommand>(infos.LastOrDefault().ProdutosVenda);
            //    item.parcelas = logCommand.parcelas;
            //    item.qntItems = logCommand.produtos.Select(p => p.quantidade).Sum();
            //    foreach (var prods in logCommand.produtos)
            //    {
            //        htmlMessage += "<h6>. " + prods.quantidade.ToString() + " " + prods.nome + "</h6>";
            //        item.infoItems = htmlMessage;
                    
                    
            //    }
                


            //}
            /*
             SELECT 
Boletos.id,
boletos.Historico as descricao,
Boletos.ValorPago as valorTotal,
Boletos.DataCompensacao ,
boletos.DataPagamento as DataVenda,
FormasRecebimento.Descricao as meioPagamento
FROM Boletos
LEFT JOIN FormasRecebimento ON Boletos.FormaRecebimentoId = FormasRecebimento.Id
WHERE Boletos.DataPagamento > '2022-06-27T00:00:00'
AND Boletos.DataPagamento < '2022-06-30T23:59:59'
             */
            var totalVendas = results.Select(t => t.valorTotal).Sum();

            return Ok(new { vendas = results, totalVendas });
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
            var userId = _aspNetUser.ObterUsuarioId();
            //var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            if (command.parcelar)
            {
                decimal valorParcela = command.valorReceber / command.parcelas;
                var formaRecebimento = _db.FormasRecebimento.Find(command.formaRecebimentoId);
                for (int i = 0; i < command.parcelas; i++)
                {
                    var data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                    if(formaRecebimento.DiasParaCompensacao != null & formaRecebimento.DiasParaCompensacao != 0)
                    {
                        var dias = formaRecebimento.DiasParaCompensacao * (i + 1);
                        data = data.AddDays(Convert.ToDouble(dias));
                    }

                    var historico = "PARCELA " + (i+1).ToString() + "/" + command.parcelas.ToString() + " VENDA PRODUTOS";
                    boletos.Add(Boleto.CadastrarBoletoVendaProdutoFactory(valorParcela, valorParcela, command.digitosCartao, Core.Enumerations.TipoLancamento.Credito,
                    unidadeId, userId, historico, command.bancoId, command.formaRecebimentoId, data, command.matriculaId));
                }
               
            }
            else
            {
                var data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                boletos.Add(Boleto.CadastrarBoletoVendaProdutoFactory(command.valorReceber, command.valorRecebido, command.digitosCartao, Core.Enumerations.TipoLancamento.Credito,
                    unidadeId, userId,"VENDA PRODUTOS À VISTA", command.bancoId, command.formaRecebimentoId, data, command.matriculaId));
            }

            await _db.Boletos.AddRangeAsync(boletos);

            _db.SaveChanges();

            var json = JsonConvert.SerializeObject(command);
            var logs = new List<LogBoletos>();//.BoletoProdutoVendaLog()
            foreach (var item in boletos)
            {
                var log = LogBoletos.BoletoProdutoVendaLog(item.Id, json, Core.Enumerations.EventoBoletoLog.Recebimento, userId);
                logs.Add(log);
            }

            _db.LogBoletos.AddRange(logs);

            _db.SaveChanges();
            // vender:
            // gerar objetos boleto conforme a qnt parcelas
            // ver parcelas e dar um "foreach"
            // cada boleto gerar log salvando objeto command





            return Ok();
        }
    }

    // VendaProdutoCommand
}
