using AutoMapper;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invictus.Api.Controllers
{
    [ApiController]
    [Route("api/dev")]
    public class DesenvolvimentoController : ControllerBase
    {
        private readonly IUnidadeQueries _unidadeQueries;
        private readonly IFinanceiroQueries _finQueries;
        private readonly IMapper _mapper;
        
        public UserManager<IdentityUser> UserManager { get; set; }
        private readonly InvictusDbContext _db;
        public DesenvolvimentoController(IMapper mapper, InvictusDbContext db, UserManager<IdentityUser> userMgr, 
            IUnidadeQueries unidadeQueries, IFinanceiroQueries finQueries)
        {
            _db = db;
            UserManager = userMgr;
            _unidadeQueries = unidadeQueries;
            _finQueries = finQueries;
            _mapper = mapper;
        }

        [HttpDelete]
        [Route("deletar-turma/{turmaId}")]
        public async Task<IActionResult> DeleteTurma(Guid turmaId)
        {

            // REVER COM TABLEA NOVA PESSOAS


            //var turmasProfs = await _db.TurmasProfessores.Where(t => t.TurmaId == turmaId).ToListAsync();
            //_db.TurmasProfessores.RemoveRange(turmasProfs);

            //var turmasPrevisoes = await _db.Previsoes.Where(t => t.TurmaId == turmaId).ToListAsync();

            //_db.Previsoes.RemoveRange(turmasPrevisoes);


            //var turmasNotas = await _db.TurmasNotas.Where(t => t.TurmaId == turmaId).ToListAsync();
            //_db.TurmasNotas.RemoveRange(turmasNotas);

            //var turmasMaterias = await _db.TurmasMaterias.Where(t => t.TurmaId == turmaId).ToListAsync();
            //_db.TurmasMaterias.RemoveRange(turmasMaterias);

            //var turmasHorarios = await _db.Horarios.Where(t => t.TurmaId == turmaId).ToListAsync();
            //_db.Horarios.RemoveRange(turmasHorarios);

            //var calendarios = await _db.Calendarios.Where(c => c.TurmaId == turmaId).ToListAsync();


            //var listCalendId = calendarios.Select(c => c.Id);
            //var turmasPresencas = _db.Presencas.Where(p => listCalendId.Contains(p.CalendarioId)).ToList();

            //_db.Presencas.RemoveRange(turmasPresencas);
            //_db.Calendarios.RemoveRange(calendarios);

            //var turma = await _db.Turmas.Where(c => c.Id == turmaId).FirstOrDefaultAsync();
            //if (turma != null) _db.Turmas.Remove(turma);



            //var logTurma = await _db.LogTurmas.Where(t => t.TurmaId == turmaId).ToListAsync();
            //_db.LogTurmas.RemoveRange(logTurma);
            //_db.SaveChanges();



            //var mat = await _db.Matriculas.Where(m => m.TurmaId == turmaId).ToListAsync();

            //if (mat.Any())
            //{

            //    foreach (var item in mat)
            //    {
            //        var aluno = await _db.Pessoas.Where(a => a.Id == item.AlunoId).FirstOrDefaultAsync();

            //        if (!String.IsNullOrEmpty(aluno.Email))
            //        {
            //            var usuario = await UserManager.FindByEmailAsync(aluno.Email);

            //            if (usuario != null) await UserManager.DeleteAsync(usuario);
            //        }

            //    }

            //    var listMatIds = mat.Select(c => c.Id);

            //    var resps = await _db.Responsaveis.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

            //    var alunoAnots = await _db.AlunosAnotacoes.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

            //    var estagiosMats = await _db.MatriculasEstagios.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();
            //    var estagiosMatsIds = estagiosMats.Select(c => c.Id);

            //    var estagioDocs = await _db.DocumentosEstagio.Where(m => estagiosMatsIds.Contains(m.Id)).ToListAsync();

            //    var logMat = await _db.LogMatriculas.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

            //    var alunosDocs = await _db.AlunosDocs.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();

            //    var alunosPlanos = await _db.AlunoPlanos.Where(m => listMatIds.Contains(m.MatriculaId)).ToListAsync();
                
            //    var boletos = await _db.Boletos.Where(m => listMatIds.Contains(m.PessoaId)).ToListAsync();

            //    var listBoletosId = boletos.Select(c => c.Id);

            //    _db.Boletos.RemoveRange(boletos);               

            //    _db.AlunoPlanos.RemoveRange(alunosPlanos);
            //    _db.AlunosDocs.RemoveRange(alunosDocs);

            //    _db.Matriculas.RemoveRange(mat);

            //    _db.Responsaveis.RemoveRange(resps);

            //    _db.AlunosAnotacoes.RemoveRange(alunoAnots);

            //    _db.DocumentosEstagio.RemoveRange(estagioDocs);
            //    _db.MatriculasEstagios.RemoveRange(estagiosMats);

            //    _db.SaveChanges();
            //}


            return Ok();

        }


        [HttpDelete]
        [Route("deletar-aluno/{alunoId}")]
        public async Task<IActionResult> DeleteAluno(Guid alunoId)
        {
            var aluno = await _db.Pessoas.Where(a => a.Id == alunoId).FirstOrDefaultAsync();

            _db.Pessoas.Remove(aluno);

            await _db.SaveChangesAsync();
            // var alunoFoto = await _db.aluno.Where(a => a.a == alunoId).FirstOrDefaultAsync();

            return Ok();

        }

        [HttpDelete]
        [Route("deletar-matricula/{matId}")]
        public async Task<IActionResult> DeleteMatricula(Guid matId)
        {
            // REVER DEPOIS TABELA PESSOAS

            //var mat = await _db.Matriculas.Where(m => m.Id == matId).FirstOrDefaultAsync();

            //var calendarios = await _db.Calendarios.Where(c => c.TurmaId == mat.TurmaId).ToListAsync();

            //var listCalendId = calendarios.Select(c => c.Id);
            //var turmasPresencas = _db.Presencas.Where(p => listCalendId.Contains(p.CalendarioId) & p.AlunoId == mat.AlunoId).ToList();
            //_db.Presencas.RemoveRange(turmasPresencas);
            //var turmasNotas = await _db.TurmasNotas.Where(t => t.MatriculaId == mat.Id).ToListAsync();
            //_db.TurmasNotas.RemoveRange(turmasNotas);

            //await _db.SaveChangesAsync();

            //var resps = await _db.Responsaveis.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            //var logMat = await _db.LogMatriculas.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            //var boletos = await _db.Boletos.Where(m => m.PessoaId == matId).ToListAsync();


            //var alunoAnots = await _db.AlunosAnotacoes.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            //var estagiosMats = await _db.MatriculasEstagios.Where(m => m.MatriculaId == mat.Id).ToListAsync();
            //var estagiosMatsIds = estagiosMats.Select(c => c.Id);

            //var estagioDocs = await _db.DocumentosEstagio.Where(m => estagiosMatsIds.Contains(m.Id)).ToListAsync();



            //var aluno = await _db.Alunos.Where(a => a.Id == mat.AlunoId).FirstOrDefaultAsync();

            //if (!String.IsNullOrEmpty(aluno.Email))
            //{
            //    var usuario = await UserManager.FindByEmailAsync(aluno.Email);

            //    if (usuario != null) await UserManager.DeleteAsync(usuario);
            //}

            //var alunosDocs = await _db.AlunosDocs.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            //var alunosPlanos = await _db.AlunoPlanos.Where(m => m.MatriculaId == mat.Id).ToListAsync();

            //_db.Boletos.RemoveRange(boletos);           

            //_db.AlunoPlanos.RemoveRange(alunosPlanos);
            //_db.AlunosDocs.RemoveRange(alunosDocs);

            //_db.Matriculas.RemoveRange(mat);

            //_db.Responsaveis.RemoveRange(resps);

            //_db.AlunosAnotacoes.RemoveRange(alunoAnots);

            //_db.DocumentosEstagio.RemoveRange(estagioDocs);
            //_db.MatriculasEstagios.RemoveRange(estagiosMats);

            //try
            //{
            //    _db.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //}


            return Ok();

        }

        [HttpGet]
        [Route("atualizar-fornecedores")]
        public async Task<IActionResult> Atualizar()
        {
            var fornecedores = await _db.Pessoas.Include(p => p.Endereco).Where(p => p.TipoPessoa == "Fornecedor").ToListAsync();

            foreach (var item in fornecedores)
            {
                item.AtualizarNome();
                _db.Entry(item.Endereco).State = EntityState.Modified;
                _db.Pessoas.Update(item);
            }

            

            

            _db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("atualizar-boletos")]
        public async Task<IActionResult> AtualizarBoletos()
        {
            var hoje = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            //var boletos = await _db.Boletos.Where(b => b.Vencimento < hoje &
            //                                b.StatusBoleto == "Em aberto").ToListAsync();

            var boletosDto = await _finQueries.GetAllBoletosVencidos();

            var boletosView = new List<BoletoViewModel>();

            foreach (var item in boletosDto)
            {
                boletosView.Add(ToBoletoView(item));
            }

            //var toBoleto = ToBoletoView(conta);
            var boletos = _mapper.Map<List<Boleto>>(boletosView);

            //await _debitoRepo.EditBoleto(boleto);

            foreach (var boleto in boletos)
            {
                boleto.SetBoletoVencido();
            }

            _db.Boletos.UpdateRange(boletos);

            _db.SaveChanges();

            return Ok();
        }

        private BoletoViewModel ToBoletoView(BoletoDto boletoDto)
        {
            var respInfo = new BoletoResponseViewModel()
            {
                id_unico = boletoDto.id_unico,
                id_unico_original = boletoDto.id_unico_original,
                status = boletoDto.status,
                msg = boletoDto.msg,
                nossonumero = boletoDto.nossonumero,
                linkBoleto = boletoDto.linkBoleto,
                linkGrupo = boletoDto.linkGrupo,
                linhaDigitavel = boletoDto.linhaDigitavel,
                pedido_numero = boletoDto.pedido_numero,
                banco_numero = boletoDto.banco_numero,
                token_facilitador = boletoDto.token_facilitador,
                credencial = boletoDto.credencial//,
                                                 // boletoId = boletoDto.boletoId,
            };

            var boleto = new BoletoViewModel()
            {
                id = boletoDto.id,
                vencimento = boletoDto.vencimento,
                dataPagamento = boletoDto.dataPagamento,
                valor = boletoDto.valor,
                valorPago = boletoDto.valorPago,
                juros = boletoDto.juros,
                jurosFixo = boletoDto.jurosFixo,
                multa = boletoDto.multa,
                multaFixo = boletoDto.multaFixo,
                desconto = boletoDto.desconto,
                tipo = boletoDto.tipo,
                diasDesconto = boletoDto.diasDesconto,
                statusBoleto = boletoDto.statusBoleto,
                historico = boletoDto.historico,
                subConta = boletoDto.subConta,
                ativo = boletoDto.ativo,
                subContaId = boletoDto.subContaId,
                bancoId = boletoDto.bancoId,
                centroCustoId = boletoDto.centroCustoId,
                meioPagamentoId = boletoDto.meioPagamentoId,
                formaPagamento = boletoDto.formaPagamento,
                digitosCartao = boletoDto.digitosCartao,
                ehFornecedor = boletoDto.ehFornecedor,
                tipoPessoa = boletoDto.tipoPessoa,
                pessoaId = boletoDto.pessoaId,
                dataCadastro = boletoDto.dataCadastro,
                reparcelamentoId = boletoDto.reparcelamentoId,
                centroCustoUnidadeId = boletoDto.centroCustoUnidadeId,
                responsavelCadastroId = boletoDto.responsavelCadastroId,
                infoBoletos = respInfo
            };

            return boleto;
        }
    }
}
