using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers.Financeiro
{
    [Route("api/financeiro")]
    [Authorize]
    [ApiController]
    public class FinanceiroController : ControllerBase
    {
        private readonly IFinanceiroQueries _finQueries;
        private readonly IAspNetUser _aspNetUser;
        private readonly ITurmaPedagQueries _turmaQueries;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly InvictusDbContext _db;
        public FinanceiroController(IFinanceiroQueries finQueries, ITurmaPedagQueries turmaQueries, InvictusDbContext db, IAspNetUser aspNetUser,
            IUnidadeQueries unidadeQueries)
        {
            _finQueries = finQueries;
            _turmaQueries = turmaQueries;
            _db = db;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
        }


        [HttpGet]
        [Route("debitos/{matriculaId}")]
        public async Task<IActionResult> AlunoDebitosV2(Guid matriculaId)
        {
            var debitos = await _finQueries.GetDebitoAlunos(matriculaId);
            var turma = await _turmaQueries.GetTurmaByMatriculaId(matriculaId);

            return Ok(new { debitos = debitos.OrderBy(c => c.vencimento), turma = turma });

        }

        [HttpGet]
        [Route("alunos")]
        public async Task<IActionResult> BuscarCadastroAlunoFin([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {

            var alunos = await _finQueries.GetAlunosFinanceiro(itemsPerPage, currentPage, paramsJson);
            if (alunos.Data.Count() == 0) return NotFound();

            return Ok(new { alunos = alunos });
            // OLD
            //var param = JsonConvert.DeserializeObject<ParametrosDTO>(paramsJson);
            //var unidadeId = await _db.Unidades.Where(u => u.Sigla == unidade).Select(u => u.Id).FirstOrDefaultAsync();
            //var pessoas = await _financeiroQueries.GetAlunoFin(itemsPerPage, currentPage, param, unidadeId);// _matriculaQueries.BuscaAlunos(param.email, param.cpf, param.nome);
            ////  var pessoas = await _matriculaQueries.BuscaAlunos(itemsPerPage, currentPage, parametros, unidadeId);
            //pessoas.Data = SetCPFBind(pessoas.Data);
            //return Ok(pessoas);
            //return Ok(pessoas);
        }

        [HttpGet]
        [Route("produtos-venda")]
        public async Task<IActionResult> BuscaVendaProdutos([FromQuery] int itemsPerPage, [FromQuery] int currentPage, [FromQuery] string paramsJson)
        {
            var registros = await _finQueries.GetProdutosVendaByRangeDate(itemsPerPage, currentPage, paramsJson);
            //var alunos = await _finQueries.GetAlunosFinanceiro(itemsPerPage, currentPage, paramsJson);
            if (registros.Data.Count() == 0) return NotFound();

            return Ok(registros);
        }

        [HttpPut]
        [Route("boleto-pagar")]
        public async Task<IActionResult> ReceberBoleto([FromBody] ReceberBoletoCommand command)
        {

            var boleto = await _db.Boletos.FindAsync(command.boletoId);

            // VERIFICAR NA API 
            if (!(boleto.StatusBoleto == "Vencido" || boleto.StatusBoleto == "Em aberto"))
            {
                return BadRequest();
            }

            boleto.ReceberBoleto(command.valorRecebido, command.formaRecebimento, command.digitosCartao);

            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            var usuarioId = _aspNetUser.ObterUsuarioId();


            var caixa = new Caixa(usuarioId, unidade.id, DateTime.Now, command.boletoId);

            await _db.Boletos.SingleUpdateAsync(boleto);
            await _db.Caixas.SingleUpdateAsync(caixa);

            await _db.SaveChangesAsync();

            return Ok();

        }

        [HttpPut]
        [Route("boleto-cancelar/{boletoId}")]
        public async Task<IActionResult> CancelarBoleto(Guid boletoId)
        {

            var boleto = await _db.Boletos.FindAsync(boletoId);

            // VERIFICAR NA API 
            if (!(boleto.StatusBoleto == StatusPagamento.Vencido.DisplayName ||
                boleto.StatusBoleto == StatusPagamento.EmAberto.DisplayName))

            {
                return BadRequest();
            }

            boleto.CancelarBoleto();

            await _db.Boletos.SingleUpdateAsync(boleto);

            await _db.SaveChangesAsync();

            return Ok();

        }
    }
}
