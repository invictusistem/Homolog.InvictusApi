using AutoMapper;
using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
using Invictus.Data.Context;
using Invictus.Domain.Financeiro;
using Invictus.Domain.Financeiro.Interfaces;
using Invictus.Dtos.Financeiro;
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
        private readonly IMapper _mapper;
        private readonly InvictusDbContext _db;
        public FinanceiroApp(IAspNetUser aspNetUser, IDebitosRepos debitoRepo, IMapper mapper, InvictusDbContext db)
        {
            _aspNetUser = aspNetUser;
            _debitoRepo = debitoRepo;
            _mapper = mapper;
            _db = db;
        }

        public async Task CadastrarContaPagar(CadastrarContaReceberCommand command)
        {
            var unidadeId = _aspNetUser.GetUnidadeIdDoUsuario();
            var userId = _aspNetUser.ObterUsuarioId();

            var boleto = Boleto.CadastrarContaPagarFactory(command.vencimento, command.valor, 0, 0, "", "", "", TipoLancamento.Debito, "",
                StatusPagamento.EmAberto, command.ehFornecedor, command.pessoaId, unidadeId, userId, command.historico, command.subcontaId, command.bancoId,
                command.meioPgmId,command.centrocustoId);


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

            var boleto = Boleto.CadastrarContaReceberFactory(command.vencimento, command.valor, 0, 0, "", "", "", TipoLancamento.Credito, "",
                StatusPagamento.EmAberto, command.ehFornecedor, command.pessoaId, unidadeId, userId, command.historico, command.subcontaId, command.bancoId);

            await _debitoRepo.SaveBoleto(boleto);
            try
            {
                _debitoRepo.Commit();
            }catch(Exception ex)
            {

            }

        }

        public async Task EditarContaReceber(BoletoDto boleto)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveConta(Guid contaId)
        {
            // var 
            var conta = await _db.Boletos.FindAsync(contaId);
            conta.InativarConta();
            await _debitoRepo.EditBoleto(conta);
            _db.SaveChanges();
        }
    }
}
