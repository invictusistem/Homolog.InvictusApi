using Invictus.Application.AdmApplication;
using Invictus.Application.AdmApplication.Interfaces;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Extensions;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Invictus.QueryService.PedagogicoQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFinanceiroApp _financApp;
        private readonly IBoletoService _boletoService;
        private readonly IAspNetUser _aspNetUser;
        private readonly ITurmaPedagQueries _turmaQueries;
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly InvictusDbContext _db;
        public FinanceiroController(IFinanceiroQueries finQueries, ITurmaPedagQueries turmaQueries, InvictusDbContext db, IAspNetUser aspNetUser,
            IUnidadeQueries unidadeQueries, IBoletoService boletoService, IFinanceiroApp financApp)
        {
            _finQueries = finQueries;
            _boletoService = boletoService;
            _turmaQueries = turmaQueries;
            _db = db;
            _aspNetUser = aspNetUser;
            _unidadeQueries = unidadeQueries;
            _financApp = financApp;
        }


        [HttpGet]
        [Route("debitos/{matriculaId}")]
        public async Task<IActionResult> AlunoDebitosV2(Guid matriculaId)
        {
            var debitos = await _finQueries.GetDebitoAlunos(matriculaId);
            var turma = await _turmaQueries.GetTurmaByMatriculaId(matriculaId);

            return Ok(new { debitos = debitos, turma = turma });

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
        /*
        [HttpPost]
        [Route("reparcelar")]
        public async Task<IActionResult> SaveReparcelas([FromBody] ReparcelaCommand reparcelasCommand)
        {


            var boletos = await _db.Boletos.Where(p => reparcelasCommand.debitosIds.Contains(p.Id)).ToListAsync();

            var verificar = boletos.Where(b => b.StatusBoleto != "Vencido");

            if (verificar.Any())
            {
                return BadRequest(new { mensagem = "Só é possível reparcelamento vencidos." });

            }

            boletos.ForEach(b => b.ChangeStatusToReparcelado());
            var usuarioId = _aspNetUser.ObterUsuarioId();
            var unidadeSigla = _aspNetUser.ObterUnidadeDoUsuario();
            var unidade = await _unidadeQueries.GetUnidadeBySigla(unidadeSigla);
            var infoDebito = await _db.Boletos.Where(d => d.Id == reparcelasCommand.debitosIds[0]).FirstOrDefaultAsync();

            var newDebitos = new List<Boleto>();
            var i = 0;

            foreach (var parcela in reparcelasCommand.parcelas)
            {

                var boletoRespInfo = new BoletoResponseInfo();

                var boleto = new Boleto(parcela.vencimento, parcela.valor, 0, 0, "", "", "", TipoLancamento.Credito, "", StatusPagamento.EmAberto, unidade.id, infoDebito.InformacaoDebitoId,
                    usuarioId, boletoRespInfo, DateTime.Now);

                newDebitos.Add(boleto);
                i++;
            }

            if (reparcelasCommand.valorEntrada != 0)
            {
                var boletoRespInfo = new BoletoResponseInfo();

                var boleto = new Boleto(DateTime.Now.AddDays(1), reparcelasCommand.valorEntrada, 0, 0, "", "", "", TipoLancamento.Credito, "", StatusPagamento.EmAberto, unidade.id, infoDebito.InformacaoDebitoId,
                    usuarioId, boletoRespInfo, DateTime.Now);

                boleto.SetHistorico("Entrada reparcelamento");

                newDebitos.Add(boleto);

            }


            var pessoa = new DadosPessoaDto();
            var infoFim = await _db.InformacoesDebito.Where(d => d.Id == boletos[0].InformacaoDebitoId).FirstOrDefaultAsync();
            var matricula = await _db.Matriculas.FindAsync(infoFim.MatriculaId);
            var aluno = await _db.Alunos.FindAsync(matricula.AlunoId);

            pessoa.nome = aluno.Nome;
            pessoa.telefone = "";
            pessoa.cpf = aluno.CPF;
            pessoa.logradouro = aluno.Endereco.Logradouro;
            pessoa.bairro = aluno.Endereco.Bairro;
            pessoa.cidade = aluno.Endereco.Cidade;
            pessoa.estado = aluno.Endereco.UF;
            pessoa.cep = aluno.Endereco.CEP;

            _db.Boletos.UpdateRange(boletos);
            _db.Boletos.AddRange(newDebitos);
            _db.SaveChanges();

            var boletoCount = _db.ParametrosValues.Where(l => l.ParametrosKeyId == new Guid("e27ae51b-2974-4cc5-b9e1-6acc7aa8d8a6")).FirstOrDefault();
            var qntBoletos = Convert.ToInt32(boletoCount.Value);


            foreach (var boleto in newDebitos)
            {
                qntBoletos++;
                var boletoResp = await _boletoService.GerarBoleto(boleto.Valor, boleto.Vencimento, pessoa, qntBoletos);

                var boletoInfo = new BoletoResponseInfo(boletoResp.id_unico, boletoResp.id_unico_original, boletoResp.status, boletoResp.msg, boletoResp.nossonumero,
                    boletoResp.linkBoleto, boletoResp.linkGrupo, boletoResp.linhaDigitavel, boletoResp.pedido_numero, boletoResp.banco_numero,
                    boletoResp.token_facilitador, boletoResp.credencial);

                boleto.SetInfoBoletos(boletoInfo);

            }

            _db.Boletos.UpdateRange(newDebitos);

            boletoCount.SetValue(qntBoletos.ToString());

            _db.ParametrosValues.Update(boletoCount);

            _db.SaveChanges();

            string listBoletosParcelado = GuidsToString.ParseGuidsToString(boletos.Select(b => b.Id).ToList());
            string newBoletos = GuidsToString.ParseGuidsToString(newDebitos.Select(b => b.Id).ToList());

            var logReparcelado = new Reparcelado(listBoletosParcelado, newBoletos);

            _db.Reparcelamentos.Add(logReparcelado);

            _db.SaveChanges();

            return Ok();
        }*/
        [HttpGet]
        [Route("contas/receber/{start}/{end}")]
        public async Task<IActionResult> Criar(Guid? meioPagamentoId, DateTime start, DateTime end)
        {
            await _finQueries.GetContasReceber(start, end);//.CadastrarContaReceber(command);

            return Ok();
        }

        [HttpPost]
        [Route("contas/receber")]
        public async Task<IActionResult> Criar([FromBody] CadastrarContaReceberCommand command)
        {
            await _financApp.CadastrarContaReceber(command);

            return Ok();
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
