using Invictus.Application.FinancApplication.Interfaces;
using Invictus.Core.Enumerations;
using Invictus.Core.Interfaces;
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
        public FinanceiroApp(IAspNetUser aspNetUser, IDebitosRepos debitoRepo)
        {
            _aspNetUser = aspNetUser;
            _debitoRepo = debitoRepo;
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
    }
}
