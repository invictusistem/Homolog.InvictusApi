using AutoMapper;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Administrativo.Logs;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Interfaces;
using Invictus.Dtos.AdmDtos;
using Invictus.Dtos.Financeiro;
using Invictus.QueryService.AdministrativoQueries.Interfaces;
using Invictus.QueryService.FinanceiroQueries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Application.FinancApplication
{
    public class FinanceiroApp : IFinanceiroApp
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IDebitosRepos _debitoRepo;
        private readonly IPessoaQueries _pessoaQuery;
        private readonly IFinanceiroQueries _finQueries;
        private readonly IMapper _mapper;
        private readonly InvictusDbContext _db;
        public FinanceiroApp(IAspNetUser aspNetUser, IDebitosRepos debitoRepo, IMapper mapper, InvictusDbContext db,
            IPessoaQueries pessoaQuery, IFinanceiroQueries finQueries)
        {
            _aspNetUser = aspNetUser;
            _debitoRepo = debitoRepo;
            _mapper = mapper;
            _db = db;
            _pessoaQuery = pessoaQuery;
            _finQueries = finQueries;
        }

        public async Task CadastrarContaPagar(CadastrarContaReceberCommand command)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var userId = _aspNetUser.ObterUsuarioId();

            var boleto = Boleto.CadastrarContaPagarFactory(command.vencimento, command.valor, 0, 0, "", "", 0, TipoLancamento.Debito, "",
                StatusPagamento.EmAberto, command.ehFornecedor, command.pessoaId, unidadeId, userId, command.historico, command.subcontaId, command.bancoId,
                command.meioPgmId, command.centrocustoId);


            await _debitoRepo.SaveBoleto(boleto);
            try
            {
                _debitoRepo.Commit();
            }
            catch (Exception ex)
            {

            }

            
        }

        public async Task CadastrarContaReceber(CadastrarContaReceberCommand command)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var userId = _aspNetUser.ObterUsuarioId();

            var boleto = Boleto.CadastrarContaReceberFactory(command.vencimento, command.valor, 0, 0, "", "", 0, TipoLancamento.Credito, "",
                StatusPagamento.EmAberto, command.ehFornecedor, command.pessoaId, unidadeId, userId, command.historico, command.subcontaId, command.bancoId);

            await _debitoRepo.SaveBoleto(boleto);
            try
            {
                _debitoRepo.Commit();
            }
            catch (Exception ex)
            {

            }

        }

        public async Task EditarContaReceber(BoletoDto boletoDto)
        {
            //var fornecedor = await _pessoaQuery.GetFornecedorById(boletoDto.pessoaId);

            //var aluno = new PessoaDto();
            try
            {
                var toBoleto = ToBoletoView(boletoDto);
                var boleto = _mapper.Map<Boleto>(toBoleto);

                await _debitoRepo.EditBoleto(boleto);


            }
            catch (Exception ex)
            {

            }

            _debitoRepo.Commit();
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

        public async Task RemoveConta(Guid contaId)
        {   
            var conta = await _finQueries.GetContaReceber(contaId);
            //conta.ativo = false;
            var toBoleto = ToBoletoView(conta);

            var boleto = _mapper.Map<Boleto>(toBoleto);

            boleto.CancelarBoleto();

            await _debitoRepo.EditBoleto(boleto);

            _db.SaveChanges();

        }
    }
}
